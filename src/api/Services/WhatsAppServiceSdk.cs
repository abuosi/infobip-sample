using System.Net.Http.Headers;
using System.Text;
using api.Models;
using api.Services.Interfaces;
using Infobip.Api.SDK;
using Infobip.Api.SDK.WhatsApp.Models;

namespace api.Services;

public class WhatsAppServiceSdk: IWhatsAppServiceSdk
{
    
    private readonly Configuration _config;

    public WhatsAppServiceSdk(Configuration config)
    {
        _config = config;
    }


    public async Task<string> SendText(MessageTextModel messageWhatsApp)
    {
        Random randomized = new Random();

        var configuration = new ApiClientConfiguration(_config.INFOBIP_URL, _config.INFOBIP_APIKEY);

        var client = new InfobipApiClient(configuration);

        var request = new WhatsAppTextMessageRequest
        {
            From = "447860099299",
            To = "5511981518511",
            MessageId = "MESSAGE_ID",
            Content = new WhatsAppTextContent("Teste")
        };

        var response = await client.WhatsApp.SendWhatsAppTextMessage(request);

        return "";
    }

    public Task<string> SendTemplate(WhatsAppMessageTemplate messageWhatsApp)
    {
        throw new NotImplementedException();
    }
}