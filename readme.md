# Mini Bank API

A simple .NET bank API project demonstrating SOLID principles implementation.

## Project Overview

This API simulates basic banking operations while adhering to SOLID principles:
- Single Responsibility Principle (SRP)
- Open-Closed Principle (OCP)
- Liskov Substitution Principle (LSP)
- Interface Segregation Principle (ISP)
- Dependency Inversion Principle (DIP)

## Features

- Account management
- Deposits and withdrawals
- Balance inquiries
- Transaction history

## Tech Stack

- .NET 7
- Entity Framework Core
- SQL Server
- RESTful API

## Getting Started

1. Clone repository
2. Install dependencies
3. Update connection string
4. Run migrations
5. Start the API

## API Endpoints

```
GET /api/accounts/{id}
POST /api/accounts
POST /api/transactions
GET /api/transactions/{accountId}
```

## Contributing

Please read CONTRIBUTING.md for details on our code of conduct and the process for submitting pull requests.

## License

This project is licensed under the MIT License - see the LICENSE file for details.