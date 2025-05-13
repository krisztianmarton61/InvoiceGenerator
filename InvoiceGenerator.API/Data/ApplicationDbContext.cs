using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.Shared.Models;

namespace InvoiceGenerator.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Client entity
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Address).IsRequired();
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
        });

        // Configure Invoice entity
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.InvoiceNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.IssueDate).IsRequired();
            entity.Property(e => e.DueDate).IsRequired();
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Notes).HasMaxLength(500);
            
            entity.HasOne(e => e.Client)
                  .WithMany(c => c.Invoices)
                  .HasForeignKey(e => e.ClientId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure InvoiceItem entity
        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Quantity).IsRequired().HasPrecision(18, 2);
            entity.Property(e => e.UnitPrice).IsRequired().HasPrecision(18, 2);
            entity.Property(e => e.TaxRate).IsRequired().HasPrecision(5, 2);
            entity.Property(e => e.Subtotal).IsRequired().HasPrecision(18, 2);
            entity.Property(e => e.Tax).IsRequired().HasPrecision(18, 2);

            entity.HasOne<Invoice>()
                  .WithMany(i => i.Items)
                  .HasForeignKey(e => e.InvoiceId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
} 