using System.Data.Entity;
using Romofyi.Domain;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public ApplicationDbContext() : base("DefaultConnection")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasRequired(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Product>()
            .HasRequired(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId);

        modelBuilder.Entity<ProductSize>()
            .HasKey(ps => new { ps.ProductId, ps.SizeId });

        modelBuilder.Entity<ProductSize>()
            .HasRequired(ps => ps.Product)
            .WithMany(p => p.ProductSizes)
            .HasForeignKey(ps => ps.ProductId);

        modelBuilder.Entity<ProductSize>()
            .HasRequired(ps => ps.Size)
            .WithMany(s => s.ProductSizes)
            .HasForeignKey(ps => ps.SizeId);

        modelBuilder.Entity<OrderItem>()
            .HasRequired(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasRequired(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId);

        modelBuilder.Entity<OrderItem>()
            .HasRequired(oi => oi.Size)
            .WithMany()
            .HasForeignKey(oi => oi.SizeId);
    }
}