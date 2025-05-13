namespace InvoiceGenerator.Shared.DTOs
{
    public class ClientStatisticsDto
    {
        public int TotalClients { get; set; }
    }

    public class InvoiceStatisticsDto
    {
        public int TotalInvoices { get; set; }
        public decimal TotalRevenue { get; set; }
    }
} 