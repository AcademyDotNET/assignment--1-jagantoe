using Assignment.DataAccess;
using Assignment.DataAccess.DependencyInjection;
using Assignment.Logic.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC_Assignment.Mail;
using MVC_Assignment.Models;

namespace MVC_Assignment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AssignmentContext>(options => options.UseSqlServer(Configuration.GetConnectionString("db")));

            services.AddSingleton(Configuration.GetSection("Smtp").Get<SmtpSettings>());
            services.AddSingleton<IMailer, Mailer>();

            services.AddRepositories();
            services.AddServices();

            services.AddAuthentication("cookieauthentication").AddCookie("cookieauthentication", settings =>
            {
                settings.Cookie.Name = "AuthCookie";
                settings.LoginPath = "/User/Login";
            });

            services.AddControllersWithViews()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<BuyProductValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
