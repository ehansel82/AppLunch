using System.Threading.Tasks;

namespace AppLunch.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string body);
    }
}