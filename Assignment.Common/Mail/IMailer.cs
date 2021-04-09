using MimeKit;
using System.Threading.Tasks;

namespace Assignment.Common.Mail
{
    public interface IMailer
    {
        Task SendMailAsync(MimeMessage mail);
    }
}
