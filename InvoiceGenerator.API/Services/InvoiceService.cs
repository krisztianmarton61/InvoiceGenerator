using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.API.Repositories.Interfaces;
using InvoiceGenerator.API.Services.Interfaces;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return invoices.Select(MapToDto);
        }

        public async Task<InvoiceDto> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            return invoice != null ? MapToDto(invoice) : null;
        }

        public async Task<Invoice> CreateInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            var invoice = await _invoiceRepository.CreateAsync(invoiceDto);
            return invoice;
        }

        public async Task<InvoiceDto> UpdateInvoiceAsync(InvoiceDto invoiceDto)
        {
            var invoice = new Invoice
            {
                Id = invoiceDto.Id,
                InvoiceNumber = invoiceDto.InvoiceNumber,
                ClientId = invoiceDto.ClientId,
                IssueDate = invoiceDto.IssueDate,
                DueDate = invoiceDto.DueDate,
                Status = invoiceDto.Status,
                Notes = invoiceDto.Notes
            };

            var updatedInvoice = await _invoiceRepository.UpdateAsync(invoice);
            return updatedInvoice != null ? MapToDto(updatedInvoice) : null;
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            await _invoiceRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<InvoiceDto>> GetInvoicesByClientIdAsync(int clientId)
        {
            var invoices = await _invoiceRepository.GetByClientIdAsync(clientId);
            return invoices.Select(MapToDto);
        }

        public async Task<InvoiceStatisticsDto> GetInvoiceStatisticsAsync()
        {
            return await _invoiceRepository.GetStatisticsAsync();
        }

        private static InvoiceDto MapToDto(Invoice invoice)
        {
            return new InvoiceDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                ClientId = invoice.ClientId,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                Status = invoice.Status,
                Notes = invoice.Notes,
                Client = invoice.Client != null ? new ClientDto
                {
                    Id = invoice.Client.Id,
                    Name = invoice.Client.Name,
                    Email = invoice.Client.Email,
                    Address = invoice.Client.Address
                } : null,
                Items = invoice.Items?.Select(item => new InvoiceItemDto
                {
                    Id = item.Id,
                    InvoiceId = item.InvoiceId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TaxRate = item.TaxRate,
                    Subtotal = item.Subtotal,
                    Tax = item.Tax,
                    Description = item.Description
                }).ToList()
            };
        }

        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }
    }
} 