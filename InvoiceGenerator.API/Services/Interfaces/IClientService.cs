using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<IEnumerable<Client>> GetArchivedClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<Client> CreateClientAsync(Client client);
        Task<Client> UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
        Task<ClientStatisticsDto> GetClientStatisticsAsync();
    }
} 