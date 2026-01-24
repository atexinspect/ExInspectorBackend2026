namespace ExInspectorBackend.Services
{
    public class KeyRotationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<KeyRotationBackgroundService> _logger;

        public KeyRotationBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<KeyRotationBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("API Key Rotation Background Service started");

            // Wait 1 minute after startup before first check
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var apiKeyService = scope.ServiceProvider.GetRequiredService<ApiKeyService>();

                        // Check if rotation is needed
                        await apiKeyService.RotateExpiredKeysAsync();

                        // Log current key status
                        var currentKey = await apiKeyService.GetCurrentKeyAsync();
                        if (currentKey != null)
                        {
                            var daysUntilExpiry = (currentKey.Value.ExpiresAt - DateTime.UtcNow).TotalDays;
                            _logger.LogInformation(
                                "API Key Status: Expires in {Days:F1} days ({ExpiresAt:yyyy-MM-dd HH:mm} UTC)",
                                daysUntilExpiry,
                                currentKey.Value.ExpiresAt);

                            // Warn if expiring soon (within 3 days)
                            if (daysUntilExpiry < 3)
                            {
                                _logger.LogWarning(
                                    "API Key expiring soon! Only {Days:F1} days remaining",
                                    daysUntilExpiry);
                            }
                        }
                    }

                    // Check every hour
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in API Key rotation background service");

                    // Wait 5 minutes before retrying after an error
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
            }

            _logger.LogInformation("API Key Rotation Background Service stopped");
        }
    }
}