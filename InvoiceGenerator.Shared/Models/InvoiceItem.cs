using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Shared.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal TaxRate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Subtotal {get; set;} = 0;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Tax { get; set; } = 0;
        
        public string Description { get; set; }
    }
} 