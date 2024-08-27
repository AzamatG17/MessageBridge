using MessageBridge.Interfaces;
using MessageBridge.Models;
using MessageBridge.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MessageBridge.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class MessageSenderController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ISendSmsClient _sendSmsClient;
        private readonly MessageResult _messageResult;
        public MessageSenderController(IEmailSender emailSender, ISendSmsClient sendSmsClient, MessageResult messageResult)
        {
            _emailSender = emailSender;
            _messageResult = messageResult;
            _sendSmsClient = sendSmsClient;
        }

        [HttpPost("email")]
        public async Task<ActionResult<MessageResult>> SendEmail(string email, string massage, string subject)
        {
            try
            {
                bool result = await _emailSender.SendMessage(email, massage, subject);

                if (result)
                {
                    _messageResult.Code = 0;
                    _messageResult.Message = "Succes";
                    _messageResult.Success = true;
                }

                return Ok(_messageResult);
            }
            catch (InvalidSendingEmail ex)
            {
                _messageResult.Code = -1;
                _messageResult.Message = ex.Message; 
                _messageResult.Success = false;

                return Ok(_messageResult);
            }
            catch (Exception ex)
            {
                _messageResult.Code = -1;
                _messageResult.Message = ex.Message;
                _messageResult.Success = false;

                return Ok(_messageResult);
            }
        }

        [HttpPost("sms")]
        public async Task<ActionResult<MessageResult>> SendSms(string phoneNum, string message)
        {
            try
            {
                var result = await _sendSmsClient.SendSmsAfterBooking(phoneNum, message);

                if (result.Item2)
                {
                    _messageResult.Code = 0;
                    _messageResult.Message = $"Succes. {result.Item1}";
                    _messageResult.Success = true;
                }

                return Ok(_messageResult);
            }
            catch (InvalidPhoneNumberException ex)
            {
                _messageResult.Code = -1;
                _messageResult.Message = ex.Message;
                _messageResult.Success = false;

                return Ok(_messageResult);
            }
            catch (InvalidSendingSms ex)
            {
                _messageResult.Code = -1;
                _messageResult.Message = ex.Message;
                _messageResult.Success = false;

                return Ok(_messageResult);
            }
            catch (Exception ex)
            {
                _messageResult.Code = -1;
                _messageResult.Message = ex.Message;
                _messageResult.Success = false;

                return Ok(_messageResult);
            }
        }
    }
}
