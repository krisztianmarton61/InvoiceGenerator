using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Shared.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public decimal Subtotal => Items?.Sum(i => i.Subtotal) ?? 0m;

        public decimal Tax => Items?.Sum(i => i.Tax) ?? 0m;

        public decimal TotalAmount => Subtotal + Tax;

        public ClientDto Client { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public ICollection<InvoiceItemDto>? Items { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }
    }
} 