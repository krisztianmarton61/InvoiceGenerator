using InvoiceGenerator.Shared.DTOs;
using InvoiceGenerator.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/invoices";

        public InvoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<InvoiceDto>>(BaseUrl);
        }

        public async Task<InvoiceDto> GetInvoiceByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<InvoiceDto>($"{BaseUrl}/{id}");
        }

        public async Task<Invoice> CreateInvoiceAsync(CreateInvoiceDto invoice)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, invoice);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Invoice>();
        }

        public async Task<InvoiceDto> UpdateInvoiceAsync(InvoiceDto invoice)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{invoice.Id}", invoice);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InvoiceDto>();
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<InvoiceItemDto>> GetInvoiceItemsAsync(int invoiceId)
        {
            return await _httpClient.GetFromJsonAsync<List<InvoiceItemDto>>($"{BaseUrl}/{invoiceId}/items");
        }

        public async Task<InvoiceItemDto> AddInvoiceItemAsync(InvoiceItemDto item)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{item.InvoiceId}/items", item);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InvoiceItemDto>();
        }

        public async Task DeleteInvoiceItemAsync(int invoiceId, int itemId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{invoiceId}/items/{itemId}");
            response.EnsureSuccessStatusCode();
        }
    }
} 