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

        public ApiKeyController(ApiKeyService apiKeyService, IConfiguration configuration)
        {
            _apiKeyService = apiKeyService;
            _configuration = configuration;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateKey([FromHeader] string adminSecret)
        {
            //var expectedSecret = _configuration["AdminSecret"] ?? "your-admin-secret-change-this";
            var expectedSecret = _configuration["AdminSecret"] ?? "Kp8#mQ5$wR2@nT7&jV4!xB9*hC6^yD1%fG3";

            if (adminSecret != expectedSecret)
            {
                return Unauthorized(new { message = "Invalid admin credentials" });
            }

            var (key, expiresAt) = await _apiKeyService.GenerateNewKeyAsync();
            
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
            var expectedSecret = _configuration["AdminSecret"] ?? "your-admin-secret-change-this";
            
            if (adminSecret != expectedSecret)
            {
                return Unauthorized(new { message = "Invalid admin credentials" });
            }

            var currentKey = await _apiKeyService.GetCurrentKeyAsync();
            if (currentKey == null)
            {
                return NotFound(new { message = "No active API key found" });
            }

            var daysRemaining = (currentKey.Value.ExpiresAt - DateTime.UtcNow).TotalDays;

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
            var expectedSecret = _configuration["AdminSecret"] ?? "your-admin-secret-change-this";
            
            if (adminSecret != expectedSecret)
            {
                return Unauthorized(new { message = "Invalid admin credentials" });
            }

            var (key, expiresAt) = await _apiKeyService.GenerateNewKeyAsync();
            
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