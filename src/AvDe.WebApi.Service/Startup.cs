using AvDe.Persistence.DbContexts;
using AvDe.Persistence.Repositories;
using AvDe.WebApi.Service.Extensions;
using AvDe.WebApi.Service.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Linq;
using System.Net;

namespace AvDe.WebApi.Service
{
    public class Startup
    {
        private const string API_NAME = "AvDe WebApi Service";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // inspired by: https://stackoverflow.com/questions/59199593/net-core-3-0-possible-object-cycle-was-detected-which-is-not-supported/59210264#59210264
            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<AvDeDbContext>(options => options.UseInMemoryDatabase("AvDe.DemoDatabase"));

            RegisterServices(services);

            // Configure Swagger support
            services.ConfigureSwagger(API_NAME);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IWebHostEnvironment env)
        {
            // Configure Serilog support
            loggerFactory.AddSerilog();

            // Create sample data
            SeedData(app);

            // Configure Swagger support
            app.ConfigureSwagger(API_NAME);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Central exception handling
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Request.Path.StartsWithSegments("/api"))
                {
                    // fallback when no content is provided in an api response
                    if (!context.HttpContext.Response.ContentLength.HasValue || context.HttpContext.Response.ContentLength == 0)
                    {
                        context.HttpContext.Response.ContentType = "text/plain";
                        var reason = GetStatusReason(context.HttpContext.Response.StatusCode);
                        await context.HttpContext.Response.WriteAsync($"Status Code: {context.HttpContext.Response.StatusCode} - {reason}").ConfigureAwait(false);
                    }
                }
                else
                {
                    context.HttpContext.Response.ContentType = "text/plain";
                    var reason = GetStatusReason(context.HttpContext.Response.StatusCode);
                    await context.HttpContext.Response.WriteAsync($"Status Code: {context.HttpContext.Response.StatusCode}").ConfigureAwait(false);
                    await context.HttpContext.Response.WriteAsync($"Status Code: {context.HttpContext.Response.StatusCode} - {reason}").ConfigureAwait(false);
                }
            });
        }

        protected virtual void RegisterServices(IServiceCollection services)
        {
            // Register repositories
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
        }

        private void SeedData(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<AvDeDbContext>();
            // Create sample data
            DbInitializer.Initialize(context);
        }

        private string GetStatusReason(int statusCode)
        {
            var key = ((HttpStatusCode)statusCode).ToString();
            return string.Concat(key.Select((c, i) => char.IsUpper(c) && i > 0 ? " " + c.ToString() : c.ToString()));
        }
    }
}
