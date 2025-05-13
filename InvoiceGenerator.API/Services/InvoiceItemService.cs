using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.API.Repositories.Interfaces;
using InvoiceGenerator.API.Services.Interfaces;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Services
{
    public class InvoiceItemService : IInvoiceItemService
    {
        private readonly IInvoiceItemRepository _itemRepository;

        public InvoiceItemService(IInvoiceItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<InvoiceItemDto>> GetAllItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return items.Select(MapToDto);
        }

        public async Task<InvoiceItemDto> GetItemByIdAsync(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            return item != null ? MapToDto(item) : null;
        }

        public async Task<InvoiceItemDto> CreateItemAsync(InvoiceItemDto itemDto)
        {
            var item = new InvoiceItem
            {
                InvoiceId = itemDto.InvoiceId,
                Quantity = itemDto.Quantity,
                UnitPrice = itemDto.UnitPrice,
                TaxRate = itemDto.TaxRate,
                Description = itemDto.Description
            };

            CalculateItemTotals(item);
            var createdItem = await _itemRepository.CreateAsync(item);
            return MapToDto(createdItem);
        }

        public async Task<InvoiceItemDto> UpdateItemAsync(InvoiceItemDto itemDto)
        {
            var item = new InvoiceItem
            {
                Id = itemDto.Id,
                InvoiceId = itemDto.InvoiceId,
                Quantity = itemDto.Quantity,
                UnitPrice = itemDto.UnitPrice,
                TaxRate = itemDto.TaxRate,
                Description = itemDto.Description
            };

            CalculateItemTotals(item);
            var updatedItem = await _itemRepository.UpdateAsync(item);
            return updatedItem != null ? MapToDto(updatedItem) : null;
        }

        public async Task DeleteItemAsync(int id)
        {
            await _itemRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<InvoiceItemDto>> GetItemsByInvoiceIdAsync(int invoiceId)
        {
            var items = await _itemRepository.GetByInvoiceIdAsync(invoiceId);
            return items.Select(MapToDto);
        }

        private static InvoiceItemDto MapToDto(InvoiceItem item)
        {
            return new InvoiceItemDto
            {
                Id = item.Id,
                InvoiceId = item.InvoiceId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TaxRate = item.TaxRate,
                Subtotal = item.Subtotal,
                Tax = item.Tax,
                Description = item.Description
            };
        }

        private static void CalculateItemTotals(InvoiceItem item)
        {
            item.Subtotal = item.Quantity * item.UnitPrice;
            item.Tax = item.Subtotal * item.TaxRate / 100;
        }
    }
} 