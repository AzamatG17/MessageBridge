using MessageBridge.Models;

namespace MessageBridge.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendMessage(SendEmailDto sendEmailDto);
    }
}
