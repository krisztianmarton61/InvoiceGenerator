using System.Threading.Tasks;
using InvoiceGenerator.Shared.DTOs;
namespace InvoiceGenerator.Services
{
    public interface IStatisticsService
    {
        Task<ClientStatisticsDto> GetClientStatisticsAsync();
        Task<InvoiceStatisticsDto> GetInvoiceStatisticsAsync();
    }
} 