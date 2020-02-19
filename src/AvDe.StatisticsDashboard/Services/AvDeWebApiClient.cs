using AvDe.Contracts.Models.Statistics;
using AvDe.StatisticsDashboard.Services.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace AvDe.StatisticsDashboard.Services
{
    public class AvDeWebApiClient : IAvDeWebApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly AvDeWebApiOptions _avDeWebApiOptions;

        public AvDeWebApiClient(IHttpClientFactory clientFactory, IOptions<AvDeWebApiOptions> avDeWebApiOptions)
        {
            _avDeWebApiOptions = avDeWebApiOptions.Value;
            var httpClient = clientFactory.CreateClient("HttpClientWithSSLUntrusted");
            httpClient.BaseAddress = new Uri(_avDeWebApiOptions.BaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "xxx");
            httpClient.Timeout = TimeSpan.FromSeconds(_avDeWebApiOptions.HttpClientTimeoutSeconds);
            _httpClient = httpClient;
        }

        public async Task<List<SoldArticle>> GetTop10SoldArticles()
        {
            var result = new List<SoldArticle>();
            try
            {
                var httpResponse = await _httpClient.GetAsync(_avDeWebApiOptions.Top10SoldArticlesUrl).ConfigureAwait(false);
                var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    return result;
                }
                result = JsonConvert.DeserializeObject<List<SoldArticle>>(content);
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    errorMsg = string.Concat(errorMsg, ' ', ex.InnerException.Message);
                }
                Log.Error(errorMsg);
            }
            return result;
        }

        public async Task<List<SoldArticlesPerDay>> GetSoldArticlesPerDay()
        {
            var result = new List<SoldArticlesPerDay>();
            try
            {
                var httpResponse = await _httpClient.GetAsync(_avDeWebApiOptions.SoldArticlesPerDayUrl).ConfigureAwait(false);
                var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    return result;
                }
                result = JsonConvert.DeserializeObject<List<SoldArticlesPerDay>>(content);
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    errorMsg = string.Concat(errorMsg, ' ', ex.InnerException.Message);
                }
                Log.Error(errorMsg);
            }
            return result;
        }

        public async Task<List<RevenuePerDay>> GetRevenuePerDay()
        {
            var result = new List<RevenuePerDay>();
            try
            {
                var httpResponse = await _httpClient.GetAsync(_avDeWebApiOptions.RevenuePerDayUrl).ConfigureAwait(false);
                var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    return result;
                }
                result = JsonConvert.DeserializeObject<List<RevenuePerDay>>(content);
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    errorMsg = string.Concat(errorMsg, ' ', ex.InnerException.Message);
                }
                Log.Error(errorMsg);
            }
            return result;
        }

        public async Task<List<RevenuePerOrder>> GetTop10OrdersByRevenue()
        {
            var result = new List<RevenuePerOrder>();
            try
            {
                var httpResponse = await _httpClient.GetAsync(_avDeWebApiOptions.Top10OrdersByRevenueUrl).ConfigureAwait(false);
                var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    return result;
                }
                result = JsonConvert.DeserializeObject<List<RevenuePerOrder>>(content);
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    errorMsg = string.Concat(errorMsg, ' ', ex.InnerException.Message);
                }
                Log.Error(errorMsg);
            }
            return result;
        }
    }
}
