using InvoiceGenerator.Shared.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api";

        public StatisticsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ClientStatisticsDto> GetClientStatisticsAsync()
        {
            return await _httpClient.GetFromJsonAsync<ClientStatisticsDto>($"{BaseUrl}/clients/statistics");
        }

        public async Task<InvoiceStatisticsDto> GetInvoiceStatisticsAsync()
        {
            return await _httpClient.GetFromJsonAsync<InvoiceStatisticsDto>($"{BaseUrl}/invoices/statistics");
        }
    }
} 