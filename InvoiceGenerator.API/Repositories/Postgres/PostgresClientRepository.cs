using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.API.Data;
using InvoiceGenerator.API.Repositories.Interfaces;
using InvoiceGenerator.Shared.Models;
using InvoiceGenerator.Shared.DTOs;

namespace InvoiceGenerator.API.Repositories;

public class PostgresClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _context;

    public PostgresClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients
            .Include(c => c.Invoices)
            .Where(c => c.Status == "Active")
            .ToListAsync();
    }

    public async Task<Client> GetByIdAsync(int id)
    {
        return await _context.Clients
            .Include(c => c.Invoices)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Client> CreateAsync(Client client)
    {
        client.Status = "Active";
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> UpdateAsync(Client client)
    {
        var existingClient = await _context.Clients.FindAsync(client.Id);
        if (existingClient == null)
            return null;

        _context.Entry(existingClient).CurrentValues.SetValues(client);
        await _context.SaveChangesAsync();
        return existingClient;
    }

    public async Task DeleteAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            client.Status = "Archived";
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ClientStatisticsDto> GetStatisticsAsync()
    {
        var totalClients = await _context.Clients.CountAsync();

        return new ClientStatisticsDto
        {
            TotalClients = totalClients
        };
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Clients.AnyAsync(c => c.Id == id);
    }
} 