# Invoice Generator

A modern web application for managing clients and generating invoices. Built with .NET 9.0

## Features

### Client Management
- Create, read, update, and archive clients
- Soft delete functionality (clients are archived instead of deleted)
- Client statistics tracking

### Invoice Management
- Manage invoices
- Track invoice status (Draft, Sent, Paid)
- Calculate subtotals, taxes, and total amounts
- Add multiple items to invoices
- Invoice statistics

## Getting Started

### Installation

1. Clone the repository:
```bash
git clone https://github.com/krisztianmarton61/InvoiceGenerator.git
```

2. Navigate to the project directory:
```bash
cd InvoiceGenerator
```

3. Set up and run the API:
```bash
    cd InvoiceGenerator.API
    # Configure `launchSettings.json` in the ./Properties folder.
    # By default, the API will run at: https://localhost:7083 and http://localhost:5001
    # Configure 'appsettings.json' in the root folder to include the default PostgreSQL connection string.
    dotnet restore
    dotnet build
    dotnet run
```

4. Navigate to the project directory:
```bash
cd InvoiceGenerator
```

5. Set up and run the Frontend:
```bash
    cd InvoiceGenerator
    # Configure `appsettings.json` in the wwwwroot folder and set the `BackendUrl` to match where the API is running.
    dotnet restore
    dotnet build
    dotnet run
```