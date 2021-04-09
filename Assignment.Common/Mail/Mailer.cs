using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace Assignment.Common.Mail
{
    public class Mailer : IMailer
    {
        private readonly SmtpSettings _smtpSettings;

        public Mailer(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task SendMailAsync(MimeMessage mail)
        {
            using var client = new SmtpClient();

            await client.ConnectAsync(_smtpSettings.MailServer, _smtpSettings.MailPort, _smtpSettings.UseSsl);

            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await client.SendAsync(mail);
            await client.DisconnectAsync(true);
        }
    }
}
