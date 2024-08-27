using System.Net.Mail;
using System.Net;
using MessageBridge.Models.Exceptions;
using Xeptions;
using MessageBridge.Interfaces;

namespace MessageBridge.Services
{
    public class EmailSender : Interfaces.IEmailSender
    {
        private readonly ILogging _loggingBroker;

        public EmailSender(ILogging loggingBroker)
        {
            _loggingBroker = loggingBroker;
        }

        public async Task<bool> SendMessage(string email, string massage, string subject)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                try
                {
                    MailAddress fromAddress = new MailAddress("barakatexnika.uz@gmail.com", subject);
                    MailAddress toAddress = new MailAddress(email);
                    MailMessage mailMessage = new MailMessage(fromAddress, toAddress)
                    {
                        Body = massage,
                        Subject = subject
                    };

                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("barakatexnika.uz@gmail.com", "dvkx kexu wbye cgud");

                    await smtpClient.SendMailAsync(mailMessage);

                    return true;
                }
                catch (InvalidSendingEmail ex)
                {
                    SendMessageError(ex);
                    throw;
                }
                catch (Exception ex)
                {
                    SendMessageError(ex);
                    throw;
                }
            }
        }

        private void SendMessageError(Exception ex)
        {
            var xeption = new InvalidSendingEmail(new Xeption(ex.Message));
            _loggingBroker.LogError(xeption);
        }
    }
}
