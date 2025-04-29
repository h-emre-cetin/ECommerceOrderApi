using ECommerceOrderApi.Data;
using ECommerceOrderApi.Models;
using ECommerceOrderApi.Repositories;
using ECommerceOrderApi.Repositories.Interfaces;
using ECommerceOrderApi.Services;
using ECommerceOrderApi.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Register services
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//seed db with test data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Check if we already have products
    if (!dbContext.Products.Any())
    {
        // Add sample products
        dbContext.Products.AddRange(
            new Product { Name = "Laptop", Price = 1299.99m, StockQuantity = 10 },
            new Product { Name = "Smartphone", Price = 699.99m, StockQuantity = 20 },
            new Product { Name = "Headphones", Price = 149.99m, StockQuantity = 30 },
            new Product { Name = "Tablet", Price = 499.99m, StockQuantity = 15 },
            new Product { Name = "Smartwatch", Price = 249.99m, StockQuantity = 25 }
        );

        dbContext.SaveChanges();
    }
}

app.Run();