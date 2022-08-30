namespace MVPSA_V2022.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(Modelos.MailRequest mailRequest);
    }
}
