using MessageBridge.Interfaces;
using MessageBridge.Models;
using MessageBridge.Services;
using System.Configuration;

namespace MessageBridge.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services, configuration);

            return services;
        }

        private static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<MessageResult>();

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ISendSmsClient, SendSmsClient>();
            services.AddScoped<ILogging, Logging>();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddHttpClient<SendSmsClient>();
        }
    }
}
