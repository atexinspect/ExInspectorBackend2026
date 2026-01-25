using ExInspectorBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiKeyController : ControllerBase
    {
        private readonly ApiKeyService _apiKeyService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiKeyController> _logger;

        public ApiKeyController(
            ApiKeyService apiKeyService, 
            IConfiguration configuration,
            ILogger<ApiKeyController> logger)
        {
            _apiKeyService = apiKeyService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateKey([FromHeader] string adminSecret)
        {
            _logger.LogInformation("=== Generate API Key Request ===");
            
            var expectedSecret = _configuration["AdminSecret"] ?? "Kp8#mQ5$wR2@nT7&jV4!xB9*hC6^yD1%fG3";
            
            if (adminSecret != expectedSecret)
            {
                _logger.LogWarning("Unauthorized: Invalid admin credentials");
                return Unauthorized(new { message = "Invalid admin credentials" });
            }

            var (key, expiresAt) = await _apiKeyService.GenerateNewKeyAsync();
            
            _logger.LogInformation("New API key generated. Expires: {ExpiresAt}", expiresAt);
            
            return Ok(new 
            { 
                key, 
                expiresAt,
                expiresAtFormatted = expiresAt.ToString("yyyy-MM-dd HH:mm:ss UTC"),
                daysValid = 30
            });
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentKey([FromHeader] string adminSecret)
        {
            _logger.LogInformation("=== API Key Request Received ===");
            _logger.LogInformation("Received admin secret: '{Secret}'", adminSecret ?? "(null)");
            
            var expectedSecret = _configuration["AdminSecret"] ?? "Kp8#mQ5$wR2@nT7&jV4!xB9*hC6^yD1%fG3";
            _logger.LogInformation("Expected admin secret: '{Secret}'", expectedSecret);
            _logger.LogInformation("Secrets match: {Match}", adminSecret == expectedSecret);
            
            if (adminSecret != expectedSecret)
            {
                _logger.LogWarning("Unauthorized: Admin secret mismatch!");
                return Unauthorized(new { message = "Invalid admin credentials" });
            }

            var currentKey = await _apiKeyService.GetCurrentKeyAsync();
            if (currentKey == null)
            {
                _logger.LogWarning("No active API key found");
                return NotFound(new { message = "No active API key found" });
            }

            var daysRemaining = (currentKey.Value.ExpiresAt - DateTime.UtcNow).TotalDays;

            _logger.LogInformation("Current API key retrieved. Expires: {ExpiresAt}, Days remaining: {Days}", 
                currentKey.Value.ExpiresAt, daysRemaining);

            return Ok(new 
            { 
                key = currentKey.Value.Key, 
                expiresAt = currentKey.Value.ExpiresAt,
                expiresAtFormatted = currentKey.Value.ExpiresAt.ToString("yyyy-MM-dd HH:mm:ss UTC"),
                daysRemaining = Math.Round(daysRemaining, 1),
                isExpiringSoon = daysRemaining < 3
            });
        }

        [HttpPost("rotate")]
        public async Task<IActionResult> RotateNow([FromHeader] string adminSecret)
        {
            _logger.LogInformation("=== Manual API Key Rotation Requested ===");
            _logger.LogInformation("Received admin secret: '{Secret}'", adminSecret ?? "(null)");

            var expectedSecret = _configuration["AdminSecret"] ?? "Kp8#mQ5$wR2@nT7&jV4!xB9*hC6^yD1%fG3";

            if (adminSecret != expectedSecret)
            {
                _logger.LogWarning("Unauthorized: Admin secret mismatch during rotation!");
                return Unauthorized(new { message = "Invalid admin credentials" });
            }

            _logger.LogInformation("Admin authenticated - proceeding with manual rotation");

            var (key, expiresAt) = await _apiKeyService.GenerateNewKeyAsync();

            _logger.LogInformation("Manual rotation completed. New key expires: {ExpiresAt}", expiresAt);

            return Ok(new
            {
                message = "API key rotated successfully",
                newKey = key,
                expiresAt,
                expiresAtFormatted = expiresAt.ToString("yyyy-MM-dd HH:mm:ss UTC"),
                gracePeriod = "Previous key valid for 24 hours"
            });
        }
    }
}