using Xeptions;

namespace MessageBridge.Models.Exceptions
{
    public class InvalidSendingEmail : Xeption
    {
        public InvalidSendingEmail(Xeption exception)
            :base("Invalid sending email message", exception)
        { }
    }
}
