using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();
        Task<InvoiceDto> GetInvoiceByIdAsync(int id);
        Task<Invoice> CreateInvoiceAsync(CreateInvoiceDto invoice);
        Task<InvoiceDto> UpdateInvoiceAsync(InvoiceDto invoice);
        Task DeleteInvoiceAsync(int id);
        Task<IEnumerable<InvoiceDto>> GetInvoicesByClientIdAsync(int clientId);
        Task<InvoiceStatisticsDto> GetInvoiceStatisticsAsync();
    }
} 