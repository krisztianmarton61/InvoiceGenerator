using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.Shared.DTOs;
namespace InvoiceGenerator.API.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice?> GetByIdAsync(int id);
        Task<Invoice> CreateAsync(CreateInvoiceDto invoice);
        Task<Invoice?> UpdateAsync(Invoice invoice);
        Task DeleteAsync(int id);
        Task<IEnumerable<Invoice>> GetByClientIdAsync(int clientId);
        Task<InvoiceStatisticsDto> GetStatisticsAsync();
    }
} 