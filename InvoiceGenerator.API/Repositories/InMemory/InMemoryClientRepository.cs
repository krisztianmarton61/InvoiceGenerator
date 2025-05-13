using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.API.Repositories.Interfaces;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Repositories
{
    public class InMemoryClientRepository : IClientRepository
    {
        private readonly List<Client> _clients = new();
        private int _nextId = 1;

        public InMemoryClientRepository()
        {
            // Add some sample clients
            _clients.Add(new Client
            {
                Id = _nextId++,
                Name = "Acme Corporation",
                Email = "contact@acmecorp.com",
                Address = "123 Business Ave, Enterprise City, EC 12345",
                Status = "Active"
            });

            _clients.Add(new Client
            {
                Id = _nextId++,
                Name = "TechStart Inc.",
                Email = "info@techstart.com",
                Address = "456 Innovation Street, Tech Valley, TV 67890",
                Status = "Active"
            });

            _clients.Add(new Client
            {
                Id = _nextId++,
                Name = "Global Solutions Ltd.",
                Email = "support@globalsolutions.com",
                Address = "789 World Trade Center, Business District, BD 54321",
                Status = "Active"
            });
        }

        public Task<IEnumerable<Client>> GetAllAsync()
        {
            // Only return active clients by default
            return Task.FromResult(_clients.Where(c => c.Status == "Active").AsEnumerable());
        }

        public Task<Client?> GetByIdAsync(int id)
        {
            var client = _clients.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(client);
        }

        public Task<Client> CreateAsync(Client client)
        {
            client.Id = _nextId++;
            client.Status = "Active"; // Ensure new clients are active
            _clients.Add(client);
            return Task.FromResult(client);
        }

        public Task<Client?> UpdateAsync(Client client)
        {
            var existingClient = _clients.FirstOrDefault(c => c.Id == client.Id);
            if (existingClient == null)
                return Task.FromResult<Client?>(null);

            existingClient.Name = client.Name;
            existingClient.Email = client.Email;
            existingClient.Address = client.Address;
            existingClient.Status = client.Status;

            return Task.FromResult<Client?>(existingClient);
        }

        public Task DeleteAsync(int id)
        {
            var client = _clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
            {
                client.Status = "Archived"; // Archive instead of remove
            }
            return Task.CompletedTask;
        }

        public Task<ClientStatisticsDto> GetStatisticsAsync()
        {
            var statistics = new ClientStatisticsDto
            {
                TotalClients = _clients.Count(c => c.Status == "Active") // Only count active clients
            };
            return Task.FromResult(statistics);
        }
    }
} 