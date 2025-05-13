using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvoiceGenerator.API.Services.Interfaces;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDto>> GetInvoice(int id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetInvoicesByClient(int clientId)
        {
            var invoices = await _invoiceService.GetInvoicesByClientIdAsync(clientId);
            return Ok(invoices);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<InvoiceStatisticsDto>> GetInvoiceStatistics()
        {
            var statistics = await _invoiceService.GetInvoiceStatisticsAsync();
            return Ok(statistics);
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> CreateInvoice(CreateInvoiceDto invoiceDto)
        {
            var createdInvoice = await _invoiceService.CreateInvoiceAsync(invoiceDto);
            return CreatedAtAction(nameof(GetInvoice), new { id = createdInvoice.Id }, createdInvoice);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InvoiceDto>> UpdateInvoice(int id, InvoiceDto invoiceDto)
        {
            if (id != invoiceDto.Id)
            {
                return BadRequest();
            }

            var updatedInvoice = await _invoiceService.UpdateInvoiceAsync(invoiceDto);
            if (updatedInvoice == null)
            {
                return NotFound();
            }

            return Ok(updatedInvoice);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            await _invoiceService.DeleteInvoiceAsync(id);
            return NoContent();
        }
    }
} 