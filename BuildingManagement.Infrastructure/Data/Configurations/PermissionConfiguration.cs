using BuildingManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PermissionName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Description)
                .HasMaxLength(500);
            builder.HasMany(p => p.Roles)
                .WithMany(r => r.Permissions);

            builder.HasMany(tn => tn.Roles)
                .WithMany(tn => tn.Permissions)
                .UsingEntity<Dictionary<string, object>>("Role_Permission", j =>
                    j.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleID")
                    .OnDelete(DeleteBehavior.Cascade),

                    j => j.HasOne<Permission>()
                    .WithMany()
                    .HasForeignKey("Id")
                    .OnDelete(DeleteBehavior.Cascade),

                    j =>
                    {
                        j.HasKey("RoleID", "Id");
                    }
                );
        }
    }
}
