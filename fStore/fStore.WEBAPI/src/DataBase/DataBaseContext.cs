using fStore.Core;
using Microsoft.EntityFrameworkCore;


namespace fStore.WEBAPI;

public class DataBaseContext : DbContext // builder pattern
{
    private readonly IConfiguration _config;
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<OrderProduct> Orders_Products { get; set; }
    public DbSet<Image> Images { get; set; }

    // To avoid time zone error when using TimeStampInterceptor
    // without time zone
    static DataBaseContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DataBaseContext(DbContextOptions options, IConfiguration config) : base(options)
    {
        _config = config;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //user role
        modelBuilder.HasPostgresEnum<Role>();
        modelBuilder.Entity<User>(entity => entity.Property(e => e.Role).HasColumnType("role"));
        modelBuilder.Entity<User>(e =>
        {
            e.HasIndex(u => u.Email).IsUnique();
            e.HasData(SeedingData.GetUser());
        });

        //Address Seed Data
        modelBuilder.Entity<Category>(e =>
        {
            e.HasData(SeedingData.GetCategories());
        });

        //Address Seed Data
        modelBuilder.Entity<Address>(e =>
        {
            e.HasData(SeedingData.GetAddresses());
        });

        //order status
        modelBuilder.HasPostgresEnum<OrderStatus>();
        modelBuilder.Entity<Order>(entity => entity.Property(e => e.OrderStatus).HasColumnType("order_status"));

        //product
        modelBuilder.Entity<Product>().ToTable(p => p.HasCheckConstraint("CK_Product_Price_Positive", "price>=0"));
        modelBuilder.Entity<Product>().ToTable(p => p.HasCheckConstraint("CK_Product_Inventory_Positive", "inventory>=0"));
        modelBuilder.Entity<Product>(e =>
        {
            e.HasData(SeedingData.GetProducts());
        });

        //Image Seed Data
        modelBuilder.Entity<Image>(e =>
        {
            e.HasData(SeedingData.GetImages());
        });

        // OrderProduct
        // OrderProduct composit primary key
        modelBuilder.Entity<OrderProduct>().HasKey(e => new { e.OrderId, e.ProductId });
        modelBuilder.Entity<OrderProduct>().ToTable(p => p.HasCheckConstraint("CK_OrderProduct_Quntity_Positive", "quntity>=0"));
        modelBuilder.Entity<OrderProduct>().ToTable(p => p.HasCheckConstraint("CK_OrderProduct_TotalPrice_Positive", "price>=0"));

        base.OnModelCreating(modelBuilder);
    }
}
