using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvoiceGenerator.API.Services.Interfaces;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class InvoiceItemsController : ControllerBase
    {
        private readonly IInvoiceItemService _itemService;

        public InvoiceItemsController(IInvoiceItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceItemDto>>> GetItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceItemDto>> GetItem(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet("invoices/{invoiceId}/items")]
        public async Task<ActionResult<IEnumerable<InvoiceItemDto>>> GetItemsByInvoice(int invoiceId)
        {
            var items = await _itemService.GetItemsByInvoiceIdAsync(invoiceId);
            return Ok(items);
        }

        [HttpPost("invoices/{invoiceId}/items")]
        public async Task<ActionResult<InvoiceItemDto>> CreateItem(int invoiceId, InvoiceItemDto itemDto)
        {
            var createdItem = await _itemService.CreateItemAsync(itemDto);
            return CreatedAtAction(nameof(GetItem), new { id = invoiceId }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InvoiceItemDto>> UpdateItem(int id, InvoiceItemDto itemDto)
        {
            if (id != itemDto.Id)
            {
                return BadRequest();
            }

            var updatedItem = await _itemService.UpdateItemAsync(itemDto);
            if (updatedItem == null)
            {
                return NotFound();
            }

            return Ok(updatedItem);
        }

        [HttpDelete("invoices/{invoiceId}/items/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _itemService.DeleteItemAsync(id);
            return NoContent();
        }
    }
} 