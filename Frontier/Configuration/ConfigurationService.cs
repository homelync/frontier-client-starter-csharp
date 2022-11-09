using Frontier.Configuration.Sections;
using Microsoft.Extensions.Configuration;

namespace Frontier.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private IConfiguration config;

        public ConfigurationService(IConfiguration config)
        {
            this.config = config;
        }

        public ApiConfiguration GetFrontierConfiguration()
        {
            var apiConfiguration = new ApiConfiguration();
            config.GetSection("Frontier").Bind(apiConfiguration);
            return apiConfiguration;
        }

        public AuthConfiguration GetAuthConfiguration()
        {
            var authConfiguration = new AuthConfiguration();
            config.GetSection("Auth").Bind(authConfiguration);
            return authConfiguration;
        }

        public TestConfiguration GetTestConfiguration()
        {
            var testConfiguration = new TestConfiguration();
            config.GetSection("Test").Bind(testConfiguration);
            return testConfiguration;
        }
    }
}