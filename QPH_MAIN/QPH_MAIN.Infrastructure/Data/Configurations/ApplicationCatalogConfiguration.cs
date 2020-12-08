using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;
using System;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class ApplicationCatalogConfiguration : IEntityTypeConfiguration<ApplicationCatalog>
    {
        public void Configure(EntityTypeBuilder<ApplicationCatalog> builder)
        {
            builder.ToTable("ApplicationCatalog");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.id_application)
                .IsRequired()
                .HasColumnName("id_application");

            builder.Property(e => e.id_catalog)
                .IsRequired()
                .HasColumnName("id_catalog");

            builder.HasOne(ac => ac.application)
                .WithMany(a => a.catalogs)
                .HasForeignKey(ac => ac.id_application);

            builder.HasOne(ac => ac.catalog)
                .WithMany(c => c.applicationCatalogs)
                .HasForeignKey(ac => ac.id_catalog);
        }
    }
}
