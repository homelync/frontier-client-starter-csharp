using Frontier.Configuration.Sections;

namespace Frontier.Configuration
{
    public interface IConfigurationService
    {
        ApiConfiguration GetFrontierConfiguration();
        AuthConfiguration GetAuthConfiguration();
        TestConfiguration GetTestConfiguration();
    }
}