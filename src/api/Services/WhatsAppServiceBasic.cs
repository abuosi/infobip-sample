using System.Net.Http.Headers;
using System.Text;
using api.Models;
using api.Services.Interfaces;
using Infobip.Api.SDK;
using Infobip.Api.SDK.WhatsApp.Models;

namespace api.Services;

public class WhatsAppServiceBasic: IWhatsAppServiceBasic
{
    
    private readonly Configuration _config;

    public WhatsAppServiceBasic(Configuration config)
    {
        _config = config;
    }

    public async Task<string> SendText(MessageTextModel messageWhatsApp) {
        
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(_config.INFOBIP_URL);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("App", _config.INFOBIP_APIKEY);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        Random randomized = new Random();

        string message = $@"
        {{
            ""from"": ""{messageWhatsApp.Sender}"",
            ""to"": ""{messageWhatsApp.Recipient}"",
            ""messageId"": ""MESSAGE_ID"",
            ""content"": {{
                ""text"": ""{messageWhatsApp.Message}""
            }},
            ""callbackData"": ""Callback data""        
        }}";

        HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "/whatsapp/1/message/text");
        httpRequest.Content = new StringContent(message, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();

        return responseContent;
    }

    public async Task<string> SendTemplate(WhatsAppMessageTemplate messageWhatsApp) {
        
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(_config.INFOBIP_URL);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("App", _config.INFOBIP_APIKEY);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        string message = $@"
        {{
            ""messages"": [
            {{
                ""from"": ""{messageWhatsApp.Sender}"",
                ""to"": ""{messageWhatsApp.Recipient}"",
                ""content"": {{
                ""templateName"": ""{messageWhatsApp.TemplateName}"",
                ""templateData"": {messageWhatsApp.TemplateData},
                ""language"": ""en""
            }}
            }}
        ]
        }}";

        HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "/whatsapp/1/message/template");
        httpRequest.Content = new StringContent(message, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();

        return responseContent;
    }

    public async Task<string> SendTextSdk(MessageTextModel messageWhatsApp)
    {
        Random randomized = new Random();

        var configuration = new ApiClientConfiguration(
            "https://XYZ.api.infobip.com",
            "YOUR_API_KEY_FROM_PORTAL");

        var client = new InfobipApiClient(configuration);

        var request = new WhatsAppTextMessageRequest
        {
            From = messageWhatsApp.Sender,
            To = messageWhatsApp.Recipient,
            MessageId = "MESSAGE_ID",
            Content = new WhatsAppTextContent(messageWhatsApp.Message)
        };

        var response = await client.WhatsApp.SendWhatsAppTextMessage(request);

        return "";
    }

    public Task<string> SendTemplateSdk(WhatsAppMessageTemplate messageWhatsApp)
    {
        throw new NotImplementedException();
    }
}