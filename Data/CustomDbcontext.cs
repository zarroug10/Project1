using System;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using web.Entities;
namespace web.Data;

public class CustomDbcontext(DbContextOptions<CustomDbcontext> options) : DbContext(options)
{
     public DbSet<Customer> Users { get; set; }
     public DbSet<Order> Orders { get; set; }
     public DbSet<Product> Products  {get; set; }
     public DbSet<OrderLine> OrderLines { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<OrderLine>()
            .HasKey(pc => new { pc.ProductId, pc.OrderId});

        builder.Entity<OrderLine>()
                .HasOne(ol => ol.Order)
                .WithMany(o => o.OrderlineItem)
                .HasForeignKey(ol => ol.OrderId);

        builder.Entity<OrderLine>()
                .HasOne(ol => ol.Product)
                .WithMany(li => li.OrderlineItem)
                .HasForeignKey(ol => ol.ProductId);
        builder.Entity<Product>()
          .OwnsOne(p => p.Price, price =>
          {
              price.Property(p => p.Currency).HasColumnName("Currency");
              price.Property(p => p.Amount).HasColumnName("Amount");
          });
    }
}
