using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using System.Reflection;

namespace QPH_MAIN.Infrastructure.Data
{
    public partial class QPHContext : DbContext
    {
        public QPHContext() {}

        public QPHContext(DbContextOptions<QPHContext> options) : base(options) {}

        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationCatalog> ApplicationCatalogs { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<SystemParameters> SystemParameters { get; set; }
        public virtual DbSet<TableColumn> TableColumn { get; set; }
        public virtual DbSet<View> Views { get; set; }
        public virtual DbSet<Blacklist> Blacklist { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Tree> Tree { get; set; }
        public virtual DbSet<CatalogTree> CatalogTree { get; set; }
        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<PermissionStatus> PermissionStatuses { get; set; }
        public virtual DbSet<Enterprise> Enterprises { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Security> Securities { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Enterprise> Enterprise { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TableColumn>().HasNoKey().ToView(null);
            modelBuilder.Entity<Tree>().HasNoKey().ToView(null);
            modelBuilder.Entity<CatalogTree>().HasNoKey().ToView(null);
            modelBuilder.Entity<PermissionStatus>().HasNoKey().ToView(null);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}