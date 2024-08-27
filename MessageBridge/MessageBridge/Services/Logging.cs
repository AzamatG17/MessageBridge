using MessageBridge.Interfaces;

namespace MessageBridge.Services
{
    public class Logging : ILogging
    {
        private readonly ILogger<Logging> logger;

        public Logging(ILogger<Logging> logger)
        {
            this.logger = logger;
        }

        public void LogCritical(Exception exception)
        {
            this.logger.LogCritical(exception, exception.Message);
        }

        public void LogError(Exception exception)
        {
            this.logger.LogError(exception, exception.Message);
        }
    }
}
