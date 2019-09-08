using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TvMazeScraper.Common.ConfigurationSections;

namespace TvMazeScraper.Common.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerConfiguration(
            this IServiceCollection services,
            ServiceSettings settings)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = settings.Name,
                    Version = "v1",
                    Description = settings.Description
                });

                options.EnableAnnotations();

                options.DocInclusionPredicate((version, apiDescription) =>
                {
                    var values = apiDescription.RelativePath
                        .Split('/')
                        .Select(v => v.Replace("v{version}", version));

                    apiDescription.RelativePath = string.Join("/", values);

                    var versionParameter = apiDescription.ParameterDescriptions
                        .SingleOrDefault(p => p.Name == "version");

                    if (versionParameter != null)
                        apiDescription.ParameterDescriptions.Remove(versionParameter);

                    return true;
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }

                options.DescribeAllEnumsAsStrings();
            });
        }
    }
}
