using CafeShop.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CafeShop.Database {
    public class CafeShopContext : DbContext {
        public virtual DbSet<Merchant> Merchants => Set<Merchant>();
        public virtual DbSet<Employee> Employees => Set<Employee>();
        public virtual DbSet<Category> Categories => Set<Category>();
        public virtual DbSet<Table> Tables => Set<Table>();
        public virtual DbSet<Product> Products => Set<Product>();
        public virtual DbSet<Image> Images => Set<Image>();
        public virtual DbSet<Order> Orders => Set<Order>();
        public virtual DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

        public CafeShopContext(DbContextOptions<CafeShopContext> options) : base(options) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql("User ID=cafeshop;Password=cafeshop;Server=senazus.com;Port=5432;Database=cafeshop;Integrated Security=true;Pooling=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}