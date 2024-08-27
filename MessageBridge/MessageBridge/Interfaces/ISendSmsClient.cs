using MessageBridge.Models;

namespace MessageBridge.Interfaces
{
    public interface ISendSmsClient
    {
        Task<(string, bool)> SendSmsAfterBooking(SendSmsDto sendSmsDto);
    }
}
