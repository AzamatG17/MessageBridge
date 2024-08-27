using Xeptions;

namespace MessageBridge.Models.Exceptions
{
    public class InvalidPhoneNumberException : Xeption
    {
        public InvalidPhoneNumberException()
            :base("Phone number is required try the format: +xxxxxxxxxxxx")
        { }
    }
}
