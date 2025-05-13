using Microsoft.AspNetCore.Mvc;
using InvoiceGenerator.API.Repositories.Interfaces;
using InvoiceGenerator.Shared.DTOs;
using System.Threading.Tasks;

namespace InvoiceGenerator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        public StatisticsController(IClientRepository clientRepository, IInvoiceRepository invoiceRepository)
        {
            _clientRepository = clientRepository;
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet("clients")]
        public async Task<ActionResult<ClientStatisticsDto>> GetClientStatistics()
        {
            var statistics = await _clientRepository.GetStatisticsAsync();
            return Ok(statistics);
        }

        [HttpGet("invoices")]
        public async Task<ActionResult<InvoiceStatisticsDto>> GetInvoiceStatistics()
        {
            var statistics = await _invoiceRepository.GetStatisticsAsync();
            return Ok(statistics);
        }
    }
} 