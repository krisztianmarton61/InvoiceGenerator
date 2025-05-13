using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Shared.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Active"; // Active or Archived

        public ICollection<Invoice> Invoices { get; set; }
    }
} 