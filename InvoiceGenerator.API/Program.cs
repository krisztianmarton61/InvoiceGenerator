using InvoiceGenerator.API.Repositories;
using InvoiceGenerator.API.Repositories.Interfaces;
using InvoiceGenerator.API.Services;
using InvoiceGenerator.API.Services.Interfaces;
using InvoiceGenerator.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Add PostgreSQL Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Register repositories
builder.Services.AddScoped<IClientRepository, PostgresClientRepository>();
builder.Services.AddScoped<IInvoiceItemRepository, PostgresInvoiceItemRepository>();
builder.Services.AddScoped<IInvoiceRepository, PostgresInvoiceRepository>();

// Register services
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceItemService, InvoiceItemService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
