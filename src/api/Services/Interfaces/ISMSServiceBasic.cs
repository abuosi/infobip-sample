using api.Models;

namespace api.Services.Interfaces
{
    public interface ISMSServiceBasic
    {
        Task<string> SendText(MessageTextModel messageText);
    }
}