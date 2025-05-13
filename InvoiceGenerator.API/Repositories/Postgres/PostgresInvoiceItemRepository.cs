using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.API.Data;
using InvoiceGenerator.API.Repositories.Interfaces;
using InvoiceGenerator.Shared.Models;

namespace InvoiceGenerator.API.Repositories;

public class PostgresInvoiceItemRepository : IInvoiceItemRepository
{
    private readonly ApplicationDbContext _context;

    public PostgresInvoiceItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InvoiceItem>> GetAllAsync()
    {
        return await _context.InvoiceItems.ToListAsync();
    }

    public async Task<InvoiceItem?> GetByIdAsync(int id)
    {
        return await _context.InvoiceItems.FindAsync(id);
    }

    public async Task<IEnumerable<InvoiceItem>> GetByInvoiceIdAsync(int invoiceId)
    {
        return await _context.InvoiceItems
            .Where(i => i.InvoiceId == invoiceId)
            .ToListAsync();
    }

    public async Task<InvoiceItem> CreateAsync(InvoiceItem item)
    {
        CalculateItemTotals(item);
        await _context.InvoiceItems.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<InvoiceItem?> UpdateAsync(InvoiceItem item)
    {
        var existingItem = await _context.InvoiceItems.FindAsync(item.Id);
        if (existingItem == null)
            return null;

        _context.Entry(existingItem).CurrentValues.SetValues(item);
        CalculateItemTotals(existingItem);
        await _context.SaveChangesAsync();
        return existingItem;
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.InvoiceItems.FindAsync(id);
        if (item != null)
        {
            _context.InvoiceItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    private void CalculateItemTotals(InvoiceItem item)
    {
        item.Subtotal = item.Quantity * item.UnitPrice;
        item.Tax = item.Subtotal * item.TaxRate / 100;
    }
} 