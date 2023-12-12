using fStore.Core;
using Microsoft.EntityFrameworkCore;

namespace fStore.WEBAPI;

    public class DataBaseContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<User> Users {get;set;}

        public DataBaseContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config= config;
        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=fStore;Username=postgres;Password=1234")
        .UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasAlternateKey(u=> u.Email);
        base.OnModelCreating(modelBuilder);
    }
}
