using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Frontier.Configuration;
using Frontier.Models;
using Frontier.Services;
using Frontier.Utility;
using Frontier.Extensions;

namespace Frontier.Service
{
    public class AdvanecdFrontierService
    {
        private readonly IConfigurationService configService;
        private readonly IRestService restService;
        private readonly IRichLogger logger;

        public AdvanecdFrontierService(
            IConfigurationService configService,
            IRestService restService,
            IRichLogger logger)
        {
            this.configService = configService;
            this.restService = restService;
            this.logger = logger;
        }

        public async Task<List<Device>> GetDevices(int page = 1)
        {
            return await GetAll<Device>("device", page);
        }

        public async Task<List<Property>> GetProperties(int page = 1, string emulate = null)
        {
            return await GetAll<Property>("property", page, emulate);
        }

        public async Task<List<ReadingResult>> GetReadings(string propertyReference, DateTime date)
        {
            var url = $"{configService.GetFrontierConfiguration().BaseUrl}/v1/property/{propertyReference}/readings/?date={date.ToIsoDate()}";
            var response = await restService.PerformApiRequest(url, HttpMethod.Get);

            EnsureSuccessfulResponse(response, url);

            throw new HttpRequestException("Unable to get readings. Status code: " + response.StatusCode);
        }

        private async Task<List<T>> GetAll<T>(string resource, int page = 1, string emulate = null)
        {
            var results = new List<T>();
            var url = $"{configService.GetFrontierConfiguration().BaseUrl}/v1/{resource}?page={page}";
            var response = await restService.PerformApiRequest(url, HttpMethod.Get, emulate: emulate);

            EnsureSuccessfulResponse(response, url);

            var responseData = await response.Content.ReadAsAsync<PagedResponse<T>>();
            results.AddRange(responseData.Results);
            if (responseData.TotalPages > page)
            {
                results.AddRange((await GetAll<T>(resource, page + 1, emulate)));
            }

            return results;
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