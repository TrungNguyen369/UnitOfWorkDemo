// Program.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using UnitOfWorkDemo.Data;
using UnitOfWorkDemo.Models;
using UnitOfWorkDemo.Repositories;

namespace UnitOfWorkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("UnitOfWorkDemo"))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                // Thêm sản phẩm
                var product1 = new Product { Name = "Product 1", Price = 10.0m };
                var product2 = new Product { Name = "Product 2", Price = 20.0m };
                unitOfWork.Products.Add(product1);
                unitOfWork.Products.Add(product2);
                unitOfWork.Complete();

                // Thêm đơn hàng
                var order = new Order
                {
                    CustomerName = "Customer 1",
                    OrderDate = DateTime.Now,
                    Products = new List<Product> { product1, product2 }
                };
                unitOfWork.Orders.Add(order);
                unitOfWork.Complete();

                // Hiển thị các sản phẩm
                var products = unitOfWork.Products.GetAll();
                Console.WriteLine("Products:");
                foreach (var product in products)
                {
                    Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
                }

                // Hiển thị các đơn hàng
                var orders = unitOfWork.Orders.GetAll();
                Console.WriteLine("\nOrders:");
                foreach (var ord in orders)
                {
                    Console.WriteLine($"Order for: {ord.CustomerName}, Date: {ord.OrderDate}");
                    foreach (var prod in ord.Products)
                    {
                        Console.WriteLine($"  Product: {prod.Name}, Price: {prod.Price}");
                    }
                }
            }
        }
    }
}
