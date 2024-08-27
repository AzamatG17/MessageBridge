using System.Net.Mail;
using System.Net;

namespace MessageBridge.Services
{
    public class EmailSender : Interfaces.IEmailSender
    {
        public async Task<bool> SendMessage(string email, string massage, string subject)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                try
                {
                    MailAddress fromAddress = new MailAddress("gieosovazamat@gmail.com", "EasyQ");
                    MailAddress toAddress = new MailAddress(email);
                    MailMessage mailMessage = new MailMessage(fromAddress, toAddress)
                    {
                        Body = massage,
                        Subject = subject
                    };

                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("gieosovazamat@gmail.com", "dhrk hrxo qrec cedz");

                    await smtpClient.SendMailAsync(mailMessage);

                    return true;
                }
                //catch (InvalidSendingEmail ex)
                //{
                //    SendMessageError(ex);
                //    return false;
                //}
                catch (Exception ex)
                {
                    //SendMessageError(ex);
                    return false;
                }
            }
        }
    }
}
