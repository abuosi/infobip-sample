using api.Models;

namespace api.Services.Interfaces;

public interface IWhatsAppService
{
    Task<string> SendText(MessageTextModel messageWhatsApp);
    Task<string> SendTemplate(WhatsAppMessageTemplate messageWhatsApp);
}

public interface IWhatsAppServiceBasic : IWhatsAppService {};

public interface IWhatsAppServiceSdk : IWhatsAppService {};
