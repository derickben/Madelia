using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Madelia.Models;
namespace Madelia.Models
{
    public class MadeliaDBContext : DbContext
    {
        public MadeliaDBContext(DbContextOptions<MadeliaDBContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer -> Order (Restrict)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order -> OrderItem (Cascade)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem -> Product (Restrict)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProductCategory Composite Key
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            // Product -> ProductCategory (Cascade or Restrict)
            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade for product deletion

            // Category -> ProductCategory (Restrict)
            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict for category deletion

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123-456-7890" },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "987-654-3210" }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Electronics", Description = "Devices and gadgets" },
                new Category { CategoryId = 2, CategoryName = "Clothing", Description = "Apparel and accessories" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Smartphone", Price = 699.99m, StockQuantity = 50 },
                new Product { ProductId = 2, ProductName = "Laptop", Price = 1199.99m, StockQuantity = 20 },
                new Product { ProductId = 3, ProductName = "T-Shirt", Price = 19.99m, StockQuantity = 100 }
            );

            // Seed ProductCategory (Many-to-Many Relationships)
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductId = 1, CategoryId = 1 }, // Smartphone in Electronics
                new ProductCategory { ProductId = 2, CategoryId = 1 }, // Laptop in Electronics
                new ProductCategory { ProductId = 3, CategoryId = 2 }  // T-Shirt in Clothing
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, CustomerId = 1, OrderDate = DateTime.Now.AddDays(-2), TotalAmount = 719.97m, Status = "Pending" },
                new Order { OrderId = 2, CustomerId = 2, OrderDate = DateTime.Now.AddDays(-1), TotalAmount = 39.98m, Status = "Completed" }
            );

            // Seed OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1, Price = 699.99m }, // Smartphone in Order 1
                new OrderItem { OrderItemId = 2, OrderId = 2, ProductId = 3, Quantity = 2, Price = 19.99m }  // 2x T-Shirt in Order 2
            );
        }
    }
}
