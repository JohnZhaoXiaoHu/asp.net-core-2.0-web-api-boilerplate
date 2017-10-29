using System.Threading.Tasks;

namespace AspNetIdentityAuthorizationServer.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
