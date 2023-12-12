using fStore.Core;
using Microsoft.EntityFrameworkCore;
using Npgsql;


namespace fStore.WEBAPI;

    public class DataBaseContext : DbContext // builder pattern
    {private readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }

        public DataBaseContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("LocalDb"));
            dataSourceBuilder.MapEnum<Role>();
            var dataSource = dataSourceBuilder.Build();
            optionsBuilder
                .UseNpgsql(dataSource)
                .UseSnakeCaseNamingConvention();
            // optionsBuilder.UseNpgsql("Host=localhost;Database=shopify;Username=admin");
            base.OnConfiguring(optionsBuilder);
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Role>();
        modelBuilder.Entity<User>(entity => entity.Property(e => e.Role).HasColumnType("role"));
        modelBuilder.Entity<User>().HasAlternateKey(u=> u.Email);
        base.OnModelCreating(modelBuilder);
    }
}
