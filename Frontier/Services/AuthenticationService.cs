using System.Net.Http;
using System.Threading.Tasks;
using Frontier.Configuration;
using Frontier.Utility;

namespace Frontier.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfigurationService configurationService;
        private readonly IRichLogger logger;

        public AuthenticationService(IHttpClientFactory httpClientFactory, IConfigurationService configurationService, IRichLogger logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.configurationService = configurationService;
            this.logger = logger;
        }

        public async Task<TokenResponse> GetToken()
        {
            var config = configurationService.GetAuthConfiguration();
            var url = $"{config.BaseUrl}/oauth2?client={config.ClientId}&secret={config.Secret}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = httpClientFactory.CreateClient();
            logger.LogDebug($"Calling homelync-auth. GET {url}");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TokenResponse>();
            }

            throw new HttpRequestException("Unable to get access token. Status code: " + response.StatusCode);
        }
    }
}