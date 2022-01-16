using Microsoft.EntityFrameworkCore;
using SimpleWebApp.WebApi.Enumerations;
using SimpleWebApp.WebApi.Models;

namespace SimpleWebApp.WebApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
    { }

    public DbSet<Item> Items => Set<Item>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AddValueConversions(modelBuilder);
        AddInitialData(modelBuilder);
    }

    private static void AddValueConversions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .Property(x => x.Type)
            .HasConversion(
                v => v.ToString(),
                v => (GoodType)Enum.Parse(typeof(GoodType), v));
    }

    private static void AddInitialData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Warehouse>()
            .HasData(
                new Warehouse { Id = 1, Name = "CZ 1", Location = "Prague", Capacity = 5000 },
                new Warehouse { Id = 2, Name = "CZ 2", Location = "Brno", Capacity = 3000 },
                new Warehouse { Id = 3, Name = "GB 1", Location = "London", Capacity = 4000 }
            );

        modelBuilder.Entity<Item>()
            .HasData(
                new Item { Id = 1, Name = "T-Short", Type = GoodType.Clothes },
                new Item { Id = 2, Name = "Mobile phone", Type = GoodType.Digital, Description = "Android phone" },
                new Item { Id = 3, Name = "Pain killer", Type = GoodType.HealthCare },
                new Item { Id = 4, Name = "Chocolate", Type = GoodType.Food, Description = "Dark chocolate" },
                new Item { Id = 5, Name = "Good 1", Type = GoodType.General, Description = "General package 1" },
                new Item { Id = 6, Name = "Good 2", Type = GoodType.General, Description = "General package 2" },
                new Item { Id = 7, Name = "Good 3", Type = GoodType.General },
                new Item { Id = 8, Name = "Good 4", Type = GoodType.General },
                new Item { Id = 9, Name = "Notebook", Type = GoodType.Digital },
                new Item { Id = 10, Name = "Water", Type = GoodType.Food, Description = "In a package of 12" }
            );

        modelBuilder.Entity<Inventory>()
            .HasData(
                new Inventory { Id = 1, ItemId = 10, WarehouseId = 1, Quantity = 200, Description = "Basic" },
                new Inventory { Id = 2, ItemId = 1, WarehouseId = 2, Quantity = 1000, Description = "For fashion week" },
                new Inventory { Id = 3, ItemId = 2, WarehouseId = 1, Quantity = 100, Description = "For sale with 50% discount" }
            );
    }
}
