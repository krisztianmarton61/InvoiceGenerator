using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Shared.DTOs
{
    public class CreateInvoiceDto
    {
      [Required(ErrorMessage = "Please select a client")]
      public int ClientId { get; set; }

      [Required(ErrorMessage = "Issue date is required")]
      public DateTime IssueDate { get; set; }

      [Required(ErrorMessage = "Due date is required")]
      public DateTime DueDate { get; set; }

      public string? Notes { get; set; }
    }
} 