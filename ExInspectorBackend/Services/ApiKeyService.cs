using ExInspectorBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ExInspectorBackend.Services
{
    public class ApiKeyService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ApiKeyService> _logger;
        private const string API_KEY_PREFIX = "ApiKey_";
        private const string CURRENT_KEY = "ApiKey_Current";
        private const string KEY_EXPIRES = "ApiKey_ExpiresAt";
        private const string PREVIOUS_KEY = "ApiKey_Previous"; // Keep for grace period

        public ApiKeyService(AppDbContext context, ILogger<ApiKeyService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(string Key, DateTime ExpiresAt)> GenerateNewKeyAsync()
        {
            var newKey = GenerateSecureKey();
            var expiresAt = DateTime.UtcNow.AddDays(30);

            // Move current key to previous (grace period for clients to update)
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

            _logger.LogInformation("New API key generated. Expires at: {ExpiresAt:yyyy-MM-dd HH:mm} UTC", expiresAt);

            return (newKey, expiresAt);
        }

        public async Task<bool> ValidateKeyAsync(string key)
        {
            // Check current key
            var currentKeySetting = await _context.Settings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Description == CURRENT_KEY && s.Active);

            if (currentKeySetting != null && currentKeySetting.Notes == key)
            {
                // Check expiration
                var expiresSetting = await _context.Settings
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Description == KEY_EXPIRES);

                if (expiresSetting != null && DateTime.TryParse(expiresSetting.Notes, out var expiresAt))
                {
                    if (expiresAt > DateTime.UtcNow)
                    {
                        return true;
                    }
                }
            }

            // Check previous key (grace period - 24 hours after rotation)
            var previousKeySetting = await _context.Settings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Description == PREVIOUS_KEY && s.Active);

            if (previousKeySetting != null && previousKeySetting.Notes == key)
            {
                // Allow previous key for 24 hours after it was updated
                var gracePeriodEnd = previousKeySetting.UpdatedAt?.AddHours(24);
                if (gracePeriodEnd > DateTimeOffset.UtcNow)
                {
                    _logger.LogWarning("Client using previous API key (grace period)");
                    return true;
                }
            }

            return false;
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
}