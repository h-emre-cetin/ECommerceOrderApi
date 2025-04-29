# E-Commerce Order Management API

A RESTful API built with ASP.NET Core for managing orders in an e-commerce platform. This project follows clean code principles and SOLID design patterns.

## Features

- Create new orders with product validation and stock checking
- List all orders for a specific user
- Retrieve detailed information about a specific order
- Delete existing orders

## Tech Stack

- .NET 8.0 / ASP.NET Core
- Entity Framework Core
- SQL Server
- Swagger UI for API documentation

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server (or SQL Server Express)
- Visual Studio 2022, VS Code, or any preferred IDE

### Installation

1. Clone the repository:
   - git clone https://github.com/h-emre-cetin/ecommerce-order-api.git cd ecommerce-order-api
2. Install the required packages:
   - dotnet restore

3. Update the connection string in `appsettings.json` to point to your SQL Server instance:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ECommerceOrderDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
4. Apply database migrations:
   - dotnet ef database update
5. Run the application:
   - dotnet run
6. Access the Swagger UI at https://localhost:7001/swagger to explore the API endpoints.


### Database Setup

The application will automatically create the database and seed it with sample products on first run. If you want to manually set up the database:
 - dotnet ef migrations add InitialCreate
 - dotnet ef database update

### API Endpoints
Method	Endpoint	Description

 - GET	/api/orders/user/{userId}	Get all orders for a specific user
 - GET	/api/orders/{id}	Get order details by ID
 - POST	/api/orders	Create a new order
 - DELETE	/api/orders/{id}	Delete an order
