using ExInspectorBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;

public class ApiKeyService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ApiKeyService> _logger;
    private readonly IMemoryCache _cache;

    private const string API_KEY_PREFIX = "ApiKey_";
    private const string CURRENT_KEY = "ApiKey_Current";
    private const string KEY_EXPIRES = "ApiKey_ExpiresAt";
    private const string PREVIOUS_KEY = "ApiKey_Previous";
    private const string FALLBACK_KEY = "ApiKey_Fallback";

    // Cache keys
    private const string CACHE_FALLBACK_HASH = "ApiKey_FallbackHash";
    private const string CACHE_CURRENT_KEY = "ApiKey_CurrentCached";
    private const string CACHE_CURRENT_EXPIRES = "ApiKey_CurrentExpiresCached";
    private const string CACHE_PREVIOUS_KEY = "ApiKey_PreviousCached";

    public ApiKeyService(AppDbContext context, ILogger<ApiKeyService> logger, IMemoryCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }

    private static string HashApiKey(string apiKey)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(apiKey));
            return Convert.ToBase64String(hashBytes);
        }
    }

    public async Task<bool> ValidateKeyAsync(string key)
    {
        // 1. Check fallback key (cached hash comparison)
        var fallbackHash = await _cache.GetOrCreateAsync(CACHE_FALLBACK_HASH, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24); // Cache for 24 hours

            var setting = await _context.Settings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Description == FALLBACK_KEY && s.Active);

            return setting?.Notes;
        });

        if (fallbackHash != null)
        {
            var hashedIncomingKey = HashApiKey(key);
            if (fallbackHash == hashedIncomingKey)
            {
                _logger.LogDebug("Valid fallback API key authenticated (cached)");
                return true;
            }
        }

        // 2. Check current key (cached)
        var currentKeyData = await _cache.GetOrCreateAsync(CACHE_CURRENT_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5); // Cache for 5 minutes

            var keySetting = await _context.Settings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Description == CURRENT_KEY && s.Active);

            var expiresSetting = await _context.Settings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Description == KEY_EXPIRES);

            DateTime? expiresAt = null;
            if (expiresSetting != null && DateTime.TryParse(expiresSetting.Notes, out var parsed))
            {
                expiresAt = parsed;
            }

            return new { Key = keySetting?.Notes, ExpiresAt = expiresAt };
        });

        if (currentKeyData?.Key != null && currentKeyData.Key == key)
        {
            if (currentKeyData.ExpiresAt.HasValue && currentKeyData.ExpiresAt.Value > DateTime.UtcNow)
            {
                _logger.LogDebug("Valid current API key authenticated (cached)");
                return true;
            }
            else if (currentKeyData.ExpiresAt.HasValue)
            {
                _logger.LogWarning("Current API key has expired");
                // Invalidate cache to force refresh
                _cache.Remove(CACHE_CURRENT_KEY);
            }
        }

        // 3. Check previous key (cached - grace period)
        var previousKeyData = await _cache.GetOrCreateAsync(CACHE_PREVIOUS_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1); // Cache for 1 hour

            var setting = await _context.Settings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Description == PREVIOUS_KEY && s.Active);

            return new { Key = setting?.Notes, UpdatedAt = setting?.UpdatedAt };
        });

        if (previousKeyData?.Key != null && previousKeyData.Key == key)
        {
            var gracePeriodEnd = previousKeyData.UpdatedAt?.AddHours(24);
            if (gracePeriodEnd > DateTimeOffset.UtcNow)
            {
                _logger.LogWarning("Client using previous API key (grace period, cached)");
                return true;
            }
        }

        _logger.LogWarning("API key validation failed");
        return false;
    }

    public async Task<(string Key, DateTime ExpiresAt)> GenerateNewKeyAsync()
    {
        var newKey = GenerateSecureKey();
        var expiresAt = DateTime.UtcNow.AddDays(30);

        // Move current key to previous
        var currentKeySetting = await _context.Settings
            .FirstOrDefaultAsync(s => s.Description == CURRENT_KEY);

        if (currentKeySetting != null)
        {
            var previousKeySetting = await _context.Settings
                .FirstOrDefaultAsync(s => s.Description == PREVIOUS_KEY);

            if (previousKeySetting == null)
            {
                previousKeySetting = new Settings
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = PREVIOUS_KEY,
                    Notes = currentKeySetting.Notes,
                    Active = true
                };
                _context.Settings.Add(previousKeySetting);
            }
            else
            {
                previousKeySetting.Notes = currentKeySetting.Notes;
                previousKeySetting.Active = true;
                previousKeySetting.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }

        // Store new current key
        if (currentKeySetting == null)
        {
            currentKeySetting = new Settings
            {
                Id = Guid.NewGuid().ToString(),
                Description = CURRENT_KEY,
                Notes = newKey,
                Active = true
            };
            _context.Settings.Add(currentKeySetting);
        }
        else
        {
            currentKeySetting.Notes = newKey;
            currentKeySetting.Active = true;
            currentKeySetting.UpdatedAt = DateTimeOffset.UtcNow;
        }

        // Store expiration date
        var expiresSetting = await _context.Settings
            .FirstOrDefaultAsync(s => s.Description == KEY_EXPIRES);

        if (expiresSetting == null)
        {
            expiresSetting = new Settings
            {
                Id = Guid.NewGuid().ToString(),
                Description = KEY_EXPIRES,
                Notes = expiresAt.ToString("o"),
                Active = true
            };
            _context.Settings.Add(expiresSetting);
        }
        else
        {
            expiresSetting.Notes = expiresAt.ToString("o");
            expiresSetting.UpdatedAt = DateTimeOffset.UtcNow;
        }

        await _context.SaveChangesAsync();

        // Invalidate caches after rotation
        _cache.Remove(CACHE_CURRENT_KEY);
        _cache.Remove(CACHE_PREVIOUS_KEY);

        _logger.LogInformation("New API key generated. Expires at: {ExpiresAt:yyyy-MM-dd HH:mm} UTC", expiresAt);

        return (newKey, expiresAt);
    }

    public async Task RotateExpiredKeysAsync()
    {
        var expiresSetting = await _context.Settings
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Description == KEY_EXPIRES);

        if (expiresSetting != null && DateTime.TryParse(expiresSetting.Notes, out var expiresAt))
        {
            if (expiresAt <= DateTime.UtcNow)
            {
                _logger.LogWarning("API Key expired! Generating new key automatically");
                await GenerateNewKeyAsync();
            }
        }
    }

    public async Task<(string Key, DateTime ExpiresAt)?> GetCurrentKeyAsync()
    {
        var currentKeySetting = await _context.Settings
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Description == CURRENT_KEY && s.Active);

        var expiresSetting = await _context.Settings
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Description == KEY_EXPIRES);

        if (currentKeySetting == null || expiresSetting == null)
        {
            return null;
        }

        if (DateTime.TryParse(expiresSetting.Notes, out var expiresAt))
        {
            return (currentKeySetting.Notes, expiresAt);
        }

        return null;
    }

    private string GenerateSecureKey()
    {
        var bytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        return Convert.ToBase64String(bytes);
    }
}