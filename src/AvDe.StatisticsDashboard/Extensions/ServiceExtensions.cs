using AvDe.StatisticsDashboard.Services.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AvDe.StatisticsDashboard.Extensions
{
    public static class ServiceExtensions
    {
        public static void AppSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Host ASP.NET Core on Windows with IIS
            // https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-2.2
            services.Configure<IISOptions>(options => options.ForwardClientCertificate = false);
            services.Configure<AvDeWebApiOptions>(configuration.GetSection(nameof(AvDeWebApiOptions)));
        }
    }
}
