using Xeptions;

namespace MessageBridge.Models.Exceptions
{
    public class InvalidSendingSms : Xeption
    {
        public InvalidSendingSms(Xeption xeption)
            : base("Invalid sending sms message", xeption)
        { }
    }
}
