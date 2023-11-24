using System.Net.Http.Headers;
using System.Text;
using api.Models;
using api.Services.Interfaces;

namespace api.Services;

public class SMSServiceBasic: ISMSServiceBasic
{
    private readonly Configuration _config;

    public SMSServiceBasic(Configuration config)
    {
        _config = config;
    }
    
    public async Task<string> SendText(MessageTextModel messageText) {
        
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(_config.INFOBIP_URL);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("App", _config.INFOBIP_APIKEY);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        Random randomized = new Random();

        string message = $@"
                {{
                    ""messages"": [
                    {{
                        ""from"": ""{messageText.Sender}"",
                        ""destinations"":
                        [
                            {{
                                ""to"": ""{messageText.Recipient}""
                            }}
                    ],
                    ""text"": ""{messageText.Message}""
                    }}
                ]
                }}";

        HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "sms/2/text/advanced");
        httpRequest.Content = new StringContent(message, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(httpRequest);
        var responseContent = await response.Content.ReadAsStringAsync();

        return responseContent;
    }      
}