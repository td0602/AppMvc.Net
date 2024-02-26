using Controller_View.Models.Contacts;
using Microsoft.EntityFrameworkCore;

namespace Controller_View.Models 
{
    // Controller_View.Models.AppDbContext
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            // base.OnConfiguring(builder);
            builder.UseNpgsql(_configuration.GetConnectionString("AppMvcConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            // {
            //     var tableName = entityType.GetTableName();
            //     if (tableName.StartsWith("AspNet"))
            //     {
            //         entityType.SetTableName(tableName.Substring(6));
            //     }
            // }

        }
        // Khai báo có Property là Contact --> Tương ứng với bảng Contact trong CSDL
        public DbSet<Contact> Contacts {get; set;}

    }
}