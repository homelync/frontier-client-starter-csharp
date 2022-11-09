using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Frontier.Models;
using Frontier.Services;
using Frontier.Extensions;
using System.Net.Http.Headers;
using Frontier.Configuration;

namespace Frontier.Service
{
    public class SimpleFrontierService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfigurationService configurationService;

        public SimpleFrontierService(IHttpClientFactory httpClientFactory, IConfigurationService configurationService)
        {
            this.httpClientFactory = httpClientFactory;
            this.configurationService = configurationService;
        }

        public async Task<List<Device>> GetDevices()
        {
            var accessToken = await GetAccessToken();

            var response = await Get<PagedResponse<Device>>("device", accessToken);
            return response.Results;
        }

        public async Task<List<Property>> GetProperties()
        {
            var accessToken = await GetAccessToken();

            var response = await Get<PagedResponse<Property>>("property", accessToken);
            return response.Results;
        }

        public async Task<List<ReadingResult>> GetReadings(string propertyReference, DateTime date)
        {
            var accessToken = await GetAccessToken();

            var response = await Get<List<ReadingResult>>($"property/{propertyReference}/readings?date={date.ToIsoDate()}", accessToken);

            return response;
        }

        private async Task<T> Get<T>(string resource, string accessToken)
        {
            var frontierUrl = $"{configurationService.GetFrontierConfiguration().BaseUrl}/v1/{resource}";
            var frontierRequest = new HttpRequestMessage(HttpMethod.Get, frontierUrl);
            frontierRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var httpClient = httpClientFactory.CreateClient();
            var frontierResponse = await httpClient.SendAsync(frontierRequest);

            EnsureSuccessfulResponse(frontierResponse, frontierUrl);

            var frontierResponseContext = await frontierResponse.Content.ReadAsAsync<T>();

            return frontierResponseContext;
        }

        private async Task<string> GetAccessToken()
        {
            var authConfig = configurationService.GetAuthConfiguration();
            var authUrl = $"{authConfig.BaseUrl}/oauth2?client={authConfig.ClientId}&secret={authConfig.Secret}";
            var authRequest = new HttpRequestMessage(HttpMethod.Get, authUrl);

            var httpClient = httpClientFactory.CreateClient();
            var authResponse = await httpClient.SendAsync(authRequest);

            EnsureSuccessfulResponse(authResponse, authUrl);

            var tokenResponse = await authResponse.Content.ReadAsAsync<TokenResponse>();
            return tokenResponse.AccessToken;
        }

        private void EnsureSuccessfulResponse(HttpResponseMessage response, string url)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"HTTP Request to {url} failed with status code: " + response.StatusCode);
            }
        }
    }
}