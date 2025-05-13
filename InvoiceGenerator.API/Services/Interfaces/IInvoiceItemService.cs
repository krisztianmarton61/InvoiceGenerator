using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Services.Interfaces
{
    public interface IInvoiceItemService
    {
        Task<IEnumerable<InvoiceItemDto>> GetAllItemsAsync();
        Task<InvoiceItemDto> GetItemByIdAsync(int id);
        Task<InvoiceItemDto> CreateItemAsync(InvoiceItemDto item);
        Task<InvoiceItemDto> UpdateItemAsync(InvoiceItemDto item);
        Task DeleteItemAsync(int id);
        Task<IEnumerable<InvoiceItemDto>> GetItemsByInvoiceIdAsync(int invoiceId);
    }
} 