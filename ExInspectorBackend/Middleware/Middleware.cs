using ExInspectorBackend.Services;

namespace ExInspectorBackend.Middleware
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Middleware> _logger;

        public Middleware(RequestDelegate next, ILogger<Middleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, ApiKeyService apiKeyService)
        {

            // Normalize path (remove duplicate slashes)
            var requestPath = context.Request.Path.Value?.Replace("//", "/") ?? string.Empty;

            // Allow API key management endpoints to bypass API key validation
            if (requestPath.StartsWith("/api/apikey", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            // Get API key from headers
            var apiKey = context.Request.Headers["XMid"].FirstOrDefault() 
                         ?? context.Request.Headers["ZUMO-API-KEY"].FirstOrDefault();

            if (string.IsNullOrEmpty(apiKey))
            {
                _logger.LogWarning("API Key not provided for path: {Path}", context.Request.Path);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided.");
                return;
            }

            // Validate the API key against the database
            var isValid = await apiKeyService.ValidateKeyAsync(apiKey);
            
            if (!isValid)
            {
                _logger.LogWarning("Invalid or expired API Key for path: {Path}", context.Request.Path);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid or expired API key.");
                return;
            }

            _logger.LogDebug("Valid API key for path: {Path}", context.Request.Path);
            
            await _next(context);
        }
    }
}
