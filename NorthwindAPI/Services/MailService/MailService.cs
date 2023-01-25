using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace NorthwindAPI.Services.MailService;

public class MailService : IMailService
{

    private readonly IConfiguration _config;
    public MailService(IConfiguration config)
    {
        _config = config;
    }

    public void SendMail(MailDTO request)
    {
        MimeMessage mail = new();
        mail.From.Add(MailboxAddress.Parse("Northwind<>"));
        mail.To.Add(MailboxAddress.Parse(request.To));
        mail.Subject = request.Subject;
        mail.Body = new TextPart(TextFormat.Html) { Text = request.Body };

        using SmtpClient smtp = new();
        smtp.Connect(_config.GetSection("Mail").GetSection("Host").Value, 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(_config.GetSection("Mail").GetSection("Username").Value, _config.GetSection("Mail").GetSection("Password").Value);
        smtp.Send(mail);
        smtp.Disconnect(true);
    }
}
