using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using TvMazeScraper.Common.Extensions;

namespace TvMazeScraper.Common.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate Next { get; }
        private IHostingEnvironment HostingEnvironment { get; }
        private ILogger<ErrorHandlingMiddleware> Logger { get; }

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            IHostingEnvironment hostingEnvironment,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            Next = next;
            HostingEnvironment = hostingEnvironment;
            Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            Logger.LogError(exception, $"Unhandled exception occurs for path: {context?.Request?.Path}");

            if (context?.Response == null)
            {
                return Task.FromException(exception);
            }

            var response = new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Error = exception.Message
            };

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = response.StatusCode;
            }

            if (HostingEnvironment.IsProductionEnvironment())
            {
                response.Error = "Oops, something went wrong.";
            }

            var jsonResponse = JsonConvert.SerializeObject(response);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
