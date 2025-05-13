using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(int id);
        Task<Client> CreateAsync(Client client);
        Task<Client> UpdateAsync(Client client);
        Task DeleteAsync(int id);
        Task<ClientStatisticsDto> GetStatisticsAsync();
    }
} 