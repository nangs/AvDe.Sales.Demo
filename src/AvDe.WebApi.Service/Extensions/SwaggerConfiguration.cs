using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

namespace AvDe.WebApi.Service.Extensions
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IApplicationBuilder app, string apiName)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "AvDe WebApi UI";
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", $"{apiName}");
                c.RoutePrefix = string.Empty;
            });
        }

        public static void ConfigureSwagger(this IServiceCollection serviceCollection, string apiName, bool includeXmlDocumentation = true)
        {
            serviceCollection.AddSwaggerGen(o =>
            {
                // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#swashbuckleaspnetcoreannotations
                o.EnableAnnotations();
                o.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = $"{apiName}",
                    Version = "v1.0",
                    Description = @"## API Documentation ##
Sample AvDe WebAPI services",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Matjaž Bravc",
                        Email = "matjaz.bravc@gmail.com",
                        Url = new Uri("https://github.com/matjazbravc"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                if (includeXmlDocumentation)
                {
                    var xmlDocFile = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "AvDe.WebApi.Service.xml");
                    if (File.Exists(xmlDocFile))
                    {
                        o.IncludeXmlComments(xmlDocFile);
                    }
                }
                o.DescribeAllParametersInCamelCase();
                o.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }
    }
}
