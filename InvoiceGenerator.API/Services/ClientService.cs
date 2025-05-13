using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.API.Repositories.Interfaces;
using InvoiceGenerator.API.Services.Interfaces;
using InvoiceGenerator.Shared.DTOs;
using System.Linq;

namespace InvoiceGenerator.API.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Client>> GetArchivedClientsAsync()
        {
            var allClients = await _clientRepository.GetAllAsync();
            return allClients.Where(c => c.Status == "Archived");
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            return await _clientRepository.CreateAsync(client);
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            return await _clientRepository.UpdateAsync(client);
        }

        public async Task DeleteClientAsync(int id)
        {
            await _clientRepository.DeleteAsync(id);
        }

        public async Task<ClientStatisticsDto> GetClientStatisticsAsync()
        {
            return await _clientRepository.GetStatisticsAsync();
        }
    }
} 