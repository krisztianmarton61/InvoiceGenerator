using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Shared.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Draft, Sent, Paid

        public Client Client { get; set; }

        public ICollection<InvoiceItem> Items { get; set; }

        public decimal Subtotal => Items != null ? Items.Sum(i => i.Subtotal) : 0m;

        public decimal Tax => Items != null ? Items.Sum(i => i.Tax) : 0m;

        public decimal Total => Subtotal + Tax;

        [StringLength(500)]
        public string Notes { get; set; }
    }
} 