using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TvMazeScraper.Common.Middleware;

namespace TvMazeScraper.Common.UnitTests.Middleware
{
    [TestClass]
    public class ErrorHandlingMiddlewareTests
    {
        private IFixture fixture;

        private IHostingEnvironment hostingEnvironment;

        [TestInitialize]
        public void Init()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
            hostingEnvironment = fixture.Create<IHostingEnvironment>();
        }

        [TestMethod]
        public async Task Invoke_UnhandledException_ReturnsErrorCodeAndErrorMessage()
        {
            // Arrange.
            hostingEnvironment.EnvironmentName = "NotProd";

            var sut = new ErrorHandlingMiddleware(
                    httpContext => throw new Exception("Error"),
                    hostingEnvironment,
                    fixture.Create<ILogger<ErrorHandlingMiddleware>>());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act.
            await sut.Invoke(context);

            // Assert.
            var actual = GetErrorResponse(context);
            actual.Error.Should().Be("Error");
            actual.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task Invoke_IsProductionEnvironment_ReturnsGenericMessage()
        {
            // Arrange.
            hostingEnvironment.EnvironmentName = "Production";
            var exception = new Exception("ErrorMessage");

            var sut = new ErrorHandlingMiddleware(
                httpContext => throw exception,
                hostingEnvironment,
                fixture.Create<ILogger<ErrorHandlingMiddleware>>());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act.
            await sut.Invoke(context);

            // Assert.
            var actual = GetErrorResponse(context);
            actual.Error.Should().Be("Oops, something went wrong.");
        }

        private ErrorResponse GetErrorResponse(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using (var stream = new StreamReader(context.Response.Body))
            {
                var json = stream.ReadToEnd();

                return JsonConvert.DeserializeObject<ErrorResponse>(json);
            }
        }
    }
}
