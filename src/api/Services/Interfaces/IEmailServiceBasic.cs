using api.Models;

namespace api.Services.Interfaces;

public interface IEmailServiceBasic
{
    Task<string> SendEmail(EmailMessage message);
    
}