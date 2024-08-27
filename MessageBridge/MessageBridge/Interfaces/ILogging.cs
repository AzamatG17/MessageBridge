namespace MessageBridge.Interfaces
{
    public interface ILogging
    {
        void LogError(Exception exception);
        void LogCritical(Exception exception);
    }
}
