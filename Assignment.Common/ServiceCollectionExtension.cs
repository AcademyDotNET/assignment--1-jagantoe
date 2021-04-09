using Assignment.Common.Logger;
using Assignment.Common.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Common
{
    public static class ServiceCollectionExtension
    {
        public static void AddCommon(this IServiceCollection services, SmtpSettings smtpSettings)
        {
            services.AddScoped<IMailer, Mailer>();
            services.AddSingleton(smtpSettings);
            services.AddScoped<ICustomLogger, CustomLogger>();
        }
    }
}
