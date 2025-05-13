using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.API.Services.Interfaces;
using InvoiceGenerator.Shared.DTOs;
using System.Linq;

namespace InvoiceGenerator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients.Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Address = c.Address,
                Status = c.Status
            }));
        }

        [HttpGet("archived")]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetArchivedClients()
        {
            var clients = await _clientService.GetArchivedClientsAsync();
            return Ok(clients.Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Address = c.Address,
                Status = c.Status
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Address = client.Address,
                Status = client.Status
            });
        }

        [HttpPost]
        public async Task<ActionResult<ClientDto>> CreateClient(ClientDto clientDto)
        {
            var client = new Client
            {
                Name = clientDto.Name,
                Email = clientDto.Email,
                Address = clientDto.Address,
                Status = "Active" // New clients are always active
            };

            var createdClient = await _clientService.CreateClientAsync(client);
            return CreatedAtAction(nameof(GetClient), new { id = createdClient.Id }, new ClientDto
            {
                Id = createdClient.Id,
                Name = createdClient.Name,
                Email = createdClient.Email,
                Address = createdClient.Address,
                Status = createdClient.Status
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, ClientDto clientDto)
        {
            if (id != clientDto.Id)
            {
                return BadRequest();
            }

            var client = new Client
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Email = clientDto.Email,
                Address = clientDto.Address,
                Status = clientDto.Status
            };

            var updatedClient = await _clientService.UpdateClientAsync(client);
            if (updatedClient == null)
            {
                return NotFound();
            }

            return Ok(new ClientDto
            {
                Id = updatedClient.Id,
                Name = updatedClient.Name,
                Email = updatedClient.Email,
                Address = updatedClient.Address,
                Status = updatedClient.Status
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<ClientStatisticsDto>> GetClientStatistics()
        {
            var statistics = await _clientService.GetClientStatisticsAsync();
            return Ok(statistics);
        }
    }
} 