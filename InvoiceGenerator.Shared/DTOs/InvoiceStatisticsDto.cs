using System;

namespace InvoiceGenerator.Shared.DTOs
{
    public class InvoiceStatisticsDto
    {
        public int TotalInvoices { get; set; }
        public decimal TotalRevenue { get; set; }
    }
} 