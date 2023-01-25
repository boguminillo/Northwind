using NorthwindAPI.Models;

namespace NorthwindAPI.Services.MailService;

public interface IMailService
{
    public void SendMail(MailDTO request);
}
