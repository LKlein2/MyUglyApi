using Microsoft.EntityFrameworkCore;

namespace MyAPI.Model
{

    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>(e =>
            {
                e.ToTable("Supplier");
                e.Property(o => o.Id).HasColumnName("Id").HasColumnType("Integer");
                e.Property(o => o.Name).HasColumnName("Name");
                e.Property(o => o.Document).HasColumnName("Document");
                e.Property(o => o.SupplierType).HasColumnName("SupplierType");
                e.Property(o => o.Active).HasColumnName("Active");
                e.HasKey(p => new { p.Id });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
