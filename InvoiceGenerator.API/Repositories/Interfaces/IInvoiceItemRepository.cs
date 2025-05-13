using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceGenerator.Shared.Models;

namespace InvoiceGenerator.API.Repositories.Interfaces
{
    public interface IInvoiceItemRepository
    {
        Task<IEnumerable<InvoiceItem>> GetAllAsync();
        Task<InvoiceItem?> GetByIdAsync(int id);
        Task<IEnumerable<InvoiceItem>> GetByInvoiceIdAsync(int invoiceId);
        Task<InvoiceItem> CreateAsync(InvoiceItem item);
        Task<InvoiceItem?> UpdateAsync(InvoiceItem item);
        Task DeleteAsync(int id);
    }
} 