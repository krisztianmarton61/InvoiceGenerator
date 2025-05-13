using InvoiceGenerator.Shared.DTOs;
using InvoiceGenerator.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();
        Task<InvoiceDto> GetInvoiceByIdAsync(int id);
        Task<Invoice> CreateInvoiceAsync(CreateInvoiceDto invoice);
        Task<InvoiceDto> UpdateInvoiceAsync(InvoiceDto invoice);
        Task DeleteInvoiceAsync(int id);
        Task<List<InvoiceItemDto>> GetInvoiceItemsAsync(int invoiceId);
        Task<InvoiceItemDto> AddInvoiceItemAsync(InvoiceItemDto item);
        Task DeleteInvoiceItemAsync(int invoiceId, int itemId);
    }
} 