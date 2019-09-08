using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TvMazeScraper.Common.ConfigurationSections;
using TvMazeScraper.Common.Extensions;
using TvMazeScraper.Common.Middleware;

namespace TvMazeScraper.Common
{
    public abstract class StartupCore
    {
        protected IConfiguration Configuration { get; set; }

        protected StartupCore(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<ServiceSettings>(Configuration.GetSection(ServiceSettings.SectionName))
                .Configure<TvMazeApiSettings>(Configuration.GetSection(TvMazeApiSettings.SectionName));

            services.AddSingleton(Configuration);

            services
               .AddOptions()
               .Configure<ServiceSettings>(Configuration.GetSection(ServiceSettings.SectionName))
               .Configure<TvMazeApiSettings>(Configuration.GetSection(TvMazeApiSettings.SectionName));

            services.ConfigureDbContext(Configuration);

            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonFormatters()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var serviceSettings = Configuration.GetSection(ServiceSettings.SectionName).Get<ServiceSettings>();

            services.AddSwaggerConfiguration(serviceSettings);
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddCors();
            services.AddHttpClient();

            OnServiceConfiguration(services);
        }

        protected virtual void OnServiceConfiguration(IServiceCollection services) { }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseMiddleware<ErrorHandlingMiddleware>();

            var serviceSettings = Configuration.GetSection(ServiceSettings.SectionName).Get<ServiceSettings>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", serviceSettings.Name);
            });

            app.UseMvc();
        }
    }
}
