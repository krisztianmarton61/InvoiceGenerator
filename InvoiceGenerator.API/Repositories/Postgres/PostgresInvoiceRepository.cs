using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.API.Data;
using InvoiceGenerator.API.Repositories.Interfaces;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Repositories
{
    public class PostgresInvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public PostgresInvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.Items)
                .ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Invoice> CreateAsync(CreateInvoiceDto invoiceDto)
        {
            // Fetch the latest invoice number
            var lastInvoice = await _context.Invoices
                .OrderByDescending(i => i.Id)
                .FirstOrDefaultAsync();

            int nextNumber = 1001; // Starting point
            if (lastInvoice != null && lastInvoice.InvoiceNumber.StartsWith("INV-"))
            {
                if (int.TryParse(lastInvoice.InvoiceNumber.Substring(4), out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            var invoice = new Invoice
            {
                ClientId = invoiceDto.ClientId,
                InvoiceNumber = $"INV-{nextNumber}",
                IssueDate = DateTime.SpecifyKind(invoiceDto.IssueDate, DateTimeKind.Utc),
                DueDate = DateTime.SpecifyKind(invoiceDto.DueDate, DateTimeKind.Utc),
                Status = "Draft",
                Notes = invoiceDto.Notes,
            };

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<Invoice?> UpdateAsync(Invoice invoice)
        {
            var existingInvoice = await _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == invoice.Id);

            if (existingInvoice == null)
                return null;

            // Convert DateTime values to UTC
            invoice.IssueDate = DateTime.SpecifyKind(invoice.IssueDate, DateTimeKind.Utc);
            invoice.DueDate = DateTime.SpecifyKind(invoice.DueDate, DateTimeKind.Utc);

            _context.Entry(existingInvoice).CurrentValues.SetValues(invoice);


            await _context.SaveChangesAsync();
            return existingInvoice;
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice != null)
            {
                _context.InvoiceItems.RemoveRange(invoice.Items);
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Invoice>> GetByClientIdAsync(int clientId)
        {
            return await _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.Items)
                .Where(i => i.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<InvoiceStatisticsDto> GetStatisticsAsync()
        {
            var totalInvoices = await _context.Invoices
            .CountAsync();

            
            var totalRevenue = await _context.Invoices
            .Where(i => i.Status == "Paid")
            .SelectMany(i => i.Items)
            .SumAsync(item => item.Quantity * item.UnitPrice * (1 + item.TaxRate / 100));
            
            return new InvoiceStatisticsDto
            {
                TotalInvoices = totalInvoices,
                TotalRevenue = totalRevenue
            };
        }
    }
} 