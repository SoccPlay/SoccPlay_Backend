using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Application.Heplers.Mail;

public class MailService : IMailService
{
    private readonly IConfiguration _config;

    public MailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmail(Model.Request.Mail.Mail request)
    {
        var fromAddress = new MailAddress(_config.GetSection("EmailUsername").Value);
        var toAddress = new MailAddress(request.To);

        var smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_config.GetSection("EmailUsername").Value,
                _config.GetSection("EmailPassword").Value),
            EnableSsl = true
        };

        var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = request.Subject,
            Body = request.Body
        };

        await smtpClient.SendMailAsync(message);
    }
}