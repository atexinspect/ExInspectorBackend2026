using ExInspectorBackend.Services;

namespace ExInspectorBackend.Middleware
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private const string XMID = "XMid";
        
        public Middleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context, ApiKeyService apiKeyService)
        {
            if (!context.Request.Headers.TryGetValue(XMID, out var extractedXMid))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided.");
                return;
            }

            // First try database key (rotating keys)
            var isValid = await apiKeyService.ValidateKeyAsync(extractedXMid);

            // Fallback to appsettings.json key (for backward compatibility during migration)
            if (!isValid)
            {
                var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
                var xMid = appSettings.GetValue<string>(XMID);
                var xMid1 = FormatAPI.FormatString(xMid);

                if (!xMid1.Equals(extractedXMid))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized client.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
