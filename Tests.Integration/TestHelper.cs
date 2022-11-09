using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.IO;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Frontier.Configuration;
using Frontier.Services;
using Frontier.Utility;
using Tests.Common.Logging;
using Frontier.Service;

namespace Tests
{
    public class TestHelper
    {

        public static IConfigurationService GetConfigurationService()
        {
            return new ConfigurationService(GetConfiguration());
        }

        public static IRestService GetRestService()
        {
            var authService = new AuthenticationService(TestHelper.GetHttpClientFactory(), TestHelper.GetConfigurationService(), TestHelper.GetLogger());
            return new RestService(TestHelper.GetHttpClientFactory(), authService, TestHelper.GetLogger());
        }

        public static IConfiguration GetConfiguration()
        {
            var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrEmpty(env))
            {
                env = "Test";
            }

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env}.json")
                .Build();
        }

        public static IHttpClientFactory GetHttpClientFactory()
        {
            var configBuilder = new ConfigurationBuilder();
            var services = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            return services.GetService<IHttpClientFactory>();
        }

        public static IRichLogger GetLogger()
        {
            var configBuilder = new ConfigurationBuilder();
            var services = new ServiceCollection().AddHttpClient().AddLogging().BuildServiceProvider();
            var logger = services.GetRequiredService<ILogger<TestHelper>>();
            return new RichLogger(logger);
        }

        public static IRichLogger GetTestLogger(ITestOutputHelper outputHelper)
        {
            return new TestLogger(outputHelper);
        }
    }
}