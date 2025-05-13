using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.API.Repositories.Interfaces;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Repositories
{
    public class InMemoryInvoiceRepository : IInvoiceRepository
    {
        private readonly List<Invoice> _invoices;
        private readonly IClientRepository _clientRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private int _nextId = 1;

        public InMemoryInvoiceRepository(IClientRepository clientRepository, IInvoiceItemRepository invoiceItemRepository)
        {
            _clientRepository = clientRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _invoices = new List<Invoice>
            {
              new Invoice
              {
                  Id = 1,
                  InvoiceNumber = "INV-1001",
                  IssueDate = new DateTime(2025, 5, 1),
                  DueDate = new DateTime(2025, 5, 15),
                  ClientId = 1,
                  Status = "Pending",     
                  Notes = "Payment due in 14 days."
              },
              new Invoice
              {
                  Id = 2,
                  InvoiceNumber = "INV-1002",
                  IssueDate = new DateTime(2025, 5, 2),
                  DueDate = new DateTime(2025, 5, 16),
                  ClientId = 2,
                  Status = "Paid",
                  Notes = ""
              },
              new Invoice
              {
                  Id = 3,
                  InvoiceNumber = "INV-1003",
                  IssueDate = new DateTime(2025, 5, 3),
                  DueDate = new DateTime(2025, 5, 17),
                  ClientId = 3,
                  Status = "Draft",
                  Notes = "Follow-up required."
              },
              new Invoice
              {
                  Id = 4,
                  InvoiceNumber = "INV-1004",
                  IssueDate = new DateTime(2025, 5, 4),
                  DueDate = new DateTime(2025, 5, 18),
                  ClientId = 3,
                  Status = "Draft",
                  Notes = ""
              },
              new Invoice
              {
                  Id = 5,
                  InvoiceNumber = "INV-1005",
                  IssueDate = new DateTime(2025, 5, 5),
                  DueDate = new DateTime(2025, 5, 19),
                  ClientId = 3,
                  Status = "Draft",
                  Notes = "Final project delivery."
              }
            };
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            var invoices = _invoices.ToList();
            foreach (var invoice in invoices)
            {
                invoice.Client = await _clientRepository.GetByIdAsync(invoice.ClientId);
                invoice.Items = (await _invoiceItemRepository.GetByInvoiceIdAsync(invoice.Id)).ToList();
            }
            return invoices;
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            var invoice = _invoices.FirstOrDefault(i => i.Id == id);
            if (invoice != null)
            {
                invoice.Client = await _clientRepository.GetByIdAsync(invoice.ClientId);
                invoice.Items = (await _invoiceItemRepository.GetByInvoiceIdAsync(invoice.Id)).ToList();
            }
            return invoice;
        }

        public async Task<Invoice> CreateAsync(CreateInvoiceDto invoice)
        {
            var createdInvoice = new Invoice
            {
                Id = _nextId++,
                InvoiceNumber = $"INV-{1000 + _nextId}",
                Status = "Draft",
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                ClientId = invoice.ClientId,
                Notes = invoice.Notes
            };
            _invoices.Add(createdInvoice);
            createdInvoice.Client = await _clientRepository.GetByIdAsync(createdInvoice.ClientId);
            createdInvoice.Items = new List<InvoiceItem>();
            return createdInvoice;
        }

        public async Task<Invoice?> UpdateAsync(Invoice invoice)
        {
            var existingInvoice = _invoices.FirstOrDefault(i => i.Id == invoice.Id);
            if (existingInvoice == null)
                return null;

            existingInvoice.InvoiceNumber = invoice.InvoiceNumber;
            existingInvoice.IssueDate = invoice.IssueDate;
            existingInvoice.DueDate = invoice.DueDate;
            existingInvoice.ClientId = invoice.ClientId;
            existingInvoice.Status = invoice.Status;
            existingInvoice.Notes = invoice.Notes;

            existingInvoice.Client = await _clientRepository.GetByIdAsync(existingInvoice.ClientId);
            existingInvoice.Items = (await _invoiceItemRepository.GetByInvoiceIdAsync(existingInvoice.Id)).ToList();
            return existingInvoice;
        }

        public Task DeleteAsync(int id)
        {
            var invoice = _invoices.FirstOrDefault(i => i.Id == id);
            if (invoice != null)
            {
                _invoices.Remove(invoice);
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Invoice>> GetByClientIdAsync(int clientId)
        {
            var invoices = _invoices.Where(i => i.ClientId == clientId).ToList();
            foreach (var invoice in invoices)
            {
                invoice.Client = await _clientRepository.GetByIdAsync(invoice.ClientId);
                invoice.Items = (await _invoiceItemRepository.GetByInvoiceIdAsync(invoice.Id)).ToList();
            }
            return invoices;
        }

        public async Task<InvoiceStatisticsDto> GetStatisticsAsync()
        {
            var invoices = await GetAllAsync();
            var statistics = new InvoiceStatisticsDto
            {
                TotalInvoices = invoices.Count(),
                TotalRevenue = invoices
                    .Where(i => i.Status == "Paid")
                    .Sum(i => i.Total)
            };
            return statistics;
        }
    }
} 