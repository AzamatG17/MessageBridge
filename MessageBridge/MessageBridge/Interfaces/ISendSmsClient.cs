namespace MessageBridge.Interfaces
{
    public interface ISendSmsClient
    {
        Task<(string, bool)> SendSmsAfterBooking(string phoneNum, string message);
    }
}
