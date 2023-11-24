using System.Net.Http.Headers;
using api.Models;
using api.Services.Interfaces;

namespace api.Services;

public class EmailServiceBasic: IEmailServiceBasic
{
    private readonly Configuration _config;

    public EmailServiceBasic(Configuration config)
    {
        _config = config;
    }
    
    public async Task<string> SendEmail(EmailMessage message) {

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(_config.INFOBIP_URL);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("App", _config.INFOBIP_APIKEY);
        var request = new MultipartFormDataContent();
        request.Add(new StringContent(message.Sender), "from");
        request.Add(new StringContent(message.Recipient), "to");
        request.Add(new StringContent(message.Subject), "subject");
        request.Add(new StringContent(message.Body), "text");

        try
        {
            var response = await client.PostAsync("email/2/send", request);
            
            var responseContent = response.Content;
            string responseString = responseContent.ReadAsStringAsync().Result;
            
            return responseString;
        }
        catch (Exception)
        {
            return "";
        }        
    }


}