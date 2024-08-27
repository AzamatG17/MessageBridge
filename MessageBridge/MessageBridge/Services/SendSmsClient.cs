using MessageBridge.Interfaces;
using MessageBridge.Models;
using MessageBridge.Models.Exceptions;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using Xeptions;

namespace MessageBridge.Services
{
    public class SendSmsClient : ISendSmsClient
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "http://10.10.12.230:1920/wi/SMSCentre/send_sms.php";
        private readonly ILogging _logging;
        public SendSmsClient(HttpClient httpClient, ILogging logging)
        {
            _httpClient = httpClient;
            _logging = logging;
        }

        public async Task<(string, bool)> SendSmsAfterBooking(SendSmsDto sendSmsDto)
        {
            try
            {
                bool result = IsValidPhoneNumber(sendSmsDto.PhoneNum);

                if (!result)
                {
                    throw new InvalidPhoneNumberException();
                }

                var smsRequest = new
                {
                    cause = "Elektron navbat",
                    phone = sendSmsDto.PhoneNum,
                    msg = sendSmsDto.Message
                };

                var jsonContent = JsonConvert.SerializeObject(smsRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://10.10.12.230:1920/wi/SMSCentre/send_sms.php", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    return (responseBody, true);
                }

                return (null, false);
            }
            catch (InvalidPhoneNumberException ex) 
            {
                SendMessageError(ex);
                throw;
            }
            catch (InvalidSendingSms ex)
            {
                SendMessageError(ex);
                throw;
            }
            catch (System.Exception ex)
            {
                SendMessageError(ex);
                throw;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) && System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\+\d{12}$");
        }

        private void SendMessageError(Exception ex)
        {
            var xeption = new InvalidSendingSms(new Xeption(ex.Message));
            _logging.LogError(xeption);
        }
    }
}
