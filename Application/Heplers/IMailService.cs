namespace Application.Heplers;

public interface IMailService
{
    Task SendEmail(Model.Request.Mail.Mail request);
}