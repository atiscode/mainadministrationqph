using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class RoleViewPermissionConfiguration
    {
        public void Configure(EntityTypeBuilder<RoleViewPermission> builder)
        {
            builder.ToTable("RoleViewPermission");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.id_role)
                .IsRequired()
                .HasColumnName("id_role");

            builder.Property(e => e.id_view)
                .IsRequired()
                .HasColumnName("id_view");

            builder.Property(e => e.id_permission)
               .IsRequired()
               .HasColumnName("id_permission");

            builder.HasOne(ac => ac.role)
                .WithMany(a => a.rolePermissions)
                .HasForeignKey(ac => ac.id_role);

            builder.HasOne(ac => ac.view)
                .WithMany(c => c.roleViewPermissions)
                .HasForeignKey(ac => ac.id_view);

            builder.HasOne(ac => ac.permission)
                .WithMany(c => c.roleViewPermissions)
                .HasForeignKey(ac => ac.id_permission);

        }
    }
}
