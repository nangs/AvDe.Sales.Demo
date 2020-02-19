using System.IO;
using AvDe.WebApi.Service;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AvDe.Demo.Tests.Services
{
    /// <summary>
    /// Factory for bootstrapping an application in-memory for functional end to end tests
    /// This factory can be used to create a TestServer instance
    /// </summary>
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder().UseStartup<Startup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            base.ConfigureWebHost(builder);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            builder.ConfigureServices(services =>
            {
                // This will bypass your [Authorize] attributes and allow your action methods to run in your tests
                services.AddMvc(opts => opts.Filters.Add(new AllowAnonymousFilter()));
            });
        }
    }
}
