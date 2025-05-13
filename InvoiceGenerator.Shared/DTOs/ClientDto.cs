using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Shared.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Active"; // Active or Archived
    }
} 