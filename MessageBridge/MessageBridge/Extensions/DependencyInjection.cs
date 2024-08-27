using MessageBridge.Interfaces;
using MessageBridge.Models;
using MessageBridge.Services;

namespace MessageBridge.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            AddServices(services);

            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<MessageResult>();

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ISendSmsClient, SendSmsClient>();
            services.AddScoped<ILogging, Logging>();

            services.AddHttpClient<SendSmsClient>();
        }
    }
}
