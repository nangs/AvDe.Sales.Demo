using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AvDe.WebApi.Service.Extensions
{
    public static class ServiceExtensions
	{
        public static void AppSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IISOptions>(options => options.ForwardClientCertificate = false);
            //services.Configure<ProfiseeOptions>(configuration.GetSection(nameof(ProfiseeOptions)));
        }
    }
}
