using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Frontier.Services;
using Frontier.Utility;

namespace Frontier.Service
{
    public class RestService : IRestService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IAuthenticationService authService;
        private readonly IRichLogger logger;

        public RestService(IHttpClientFactory httpClientFactory, IAuthenticationService authService, IRichLogger logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.authService = authService;
            this.logger = logger;
        }

        public async Task<HttpResponseMessage> PerformApiRequest(string url, HttpMethod method, object body = null, string emulate = null)
        {
            var request = new HttpRequestMessage(method, url);
            var token = await authService.GetToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            if (body != null)
            {
                request.Content = new StringContent(new JsonISOSerializer().Serialize(body), Encoding.UTF8, "application/json");
            }

            var client = httpClientFactory.CreateClient();
            if (!string.IsNullOrWhiteSpace(emulate))
            {
                request.Headers.Add("x-emulate", new[] { emulate });
            }

            logger.LogDebug($"Calling frontier. Url: {method} {url}");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"HTTP Request to {url} failed with status code: " + response.StatusCode);
            }

            logger.LogDebug($"Called frontier. Url: {method} {url}");
            return response;
        }
    }
}