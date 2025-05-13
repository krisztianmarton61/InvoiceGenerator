using InvoiceGenerator.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> GetClientByIdAsync(int id);
        Task<ClientDto> CreateClientAsync(ClientDto client);
        Task<ClientDto> UpdateClientAsync(ClientDto client);
        Task DeleteClientAsync(int id);
    }
} 