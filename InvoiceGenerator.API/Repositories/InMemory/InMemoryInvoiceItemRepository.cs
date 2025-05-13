using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.API.Repositories.Interfaces;

namespace InvoiceGenerator.API.Repositories
{
    public class InMemoryInvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly List<InvoiceItem> _items;
        private int _nextId = 1;

        public InMemoryInvoiceItemRepository()
        {
            _items = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    Id = 1,
                    InvoiceId = 1,
                    Quantity = 2,
                    UnitPrice = 100.00m,
                    TaxRate = 27,
                    Description = "Web Development Services"
                },
                new InvoiceItem
                {
                    Id = 2,
                    InvoiceId = 1,
                    Quantity = 1,
                    UnitPrice = 50.00m,
                    TaxRate = 27,
                    Description = "Hosting Services"
                },
                new InvoiceItem
                {
                    Id = 3,
                    InvoiceId = 2,
                    Quantity = 5,
                    UnitPrice = 75.00m,
                    TaxRate = 27,
                    Description = "Consulting Hours"
                }
            };

            // Calculate initial subtotals and taxes
            foreach (var item in _items)
            {
                CalculateItemTotals(item);
            }
        }

        public async Task<IEnumerable<InvoiceItem>> GetAllAsync()
        {
            return _items.ToList();
        }

        public async Task<InvoiceItem?> GetByIdAsync(int id)
        {
            return _items.FirstOrDefault(i => i.Id == id);
        }

        public async Task<IEnumerable<InvoiceItem>> GetByInvoiceIdAsync(int invoiceId)
        {
            return _items.Where(i => i.InvoiceId == invoiceId).ToList();
        }

        public async Task<InvoiceItem> CreateAsync(InvoiceItem item)
        {
            item.Id = _nextId++;
            CalculateItemTotals(item);
            _items.Add(item);
            return item;
        }

        public async Task<InvoiceItem?> UpdateAsync(InvoiceItem item)
        {
            var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
                return null;

            existingItem.InvoiceId = item.InvoiceId;
            existingItem.Quantity = item.Quantity;
            existingItem.UnitPrice = item.UnitPrice;
            existingItem.TaxRate = item.TaxRate;
            existingItem.Description = item.Description;

            CalculateItemTotals(existingItem);
            return existingItem;
        }

        public Task DeleteAsync(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _items.Remove(item);
            }
            return Task.CompletedTask;
        }

        private void CalculateItemTotals(InvoiceItem item)
        {
            item.Subtotal = item.Quantity * item.UnitPrice;
            item.Tax = item.Subtotal * item.TaxRate / 100;
        }
    }
} 