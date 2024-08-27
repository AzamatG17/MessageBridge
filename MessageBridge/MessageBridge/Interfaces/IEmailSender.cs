namespace MessageBridge.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendMessage(string email, string massage, string subject);
    }
}
