using InvoiceGenerator.Shared.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/clients";

        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ClientDto>>(BaseUrl);
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ClientDto>($"{BaseUrl}/{id}");
        }

        public async Task<ClientDto> CreateClientAsync(ClientDto client)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, client);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ClientDto>();
        }

        public async Task<ClientDto> UpdateClientAsync(ClientDto client)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{client.Id}", client);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ClientDto>();
        }

        public async Task DeleteClientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
} 