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
    public class tnNhanVienConfiguration : IEntityTypeConfiguration<tnNhanVien>
    {
        public void Configure(EntityTypeBuilder<tnNhanVien> builder)
        {
            builder.HasKey(a => a.MaNV);

            builder.HasMany(tn => tn.Roles)
                .WithMany(tn => tn.tnNhanViens)
                .UsingEntity<Dictionary<string, object>>("Role_NhanVien", j =>
                    j.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleID")
                    .OnDelete(DeleteBehavior.Cascade),

                    j => j.HasOne<tnNhanVien>()
                    .WithMany()
                    .HasForeignKey("MaNV")
                    .OnDelete(DeleteBehavior.Cascade),

                    j =>
                    {
                        j.HasKey("RoleID", "MaNV");
                    }
                );

            builder.HasMany(tn => tn.tnToaNhas)
                .WithMany(tn => tn.tnNhanViens)
                .UsingEntity<Dictionary<string, object>>("ToaNha_NhanVien", tn =>
                    tn.HasOne<tnToaNha>()
                    .WithMany()
                    .HasForeignKey("MaTN")
                    .OnDelete(DeleteBehavior.Cascade),

                    tn => tn.HasOne<tnNhanVien>()
                    .WithMany()
                    .HasForeignKey("MaNV")
                    .OnDelete(DeleteBehavior.Cascade),

                    tn =>
                    {
                        tn.HasKey("MaTN", "MaNV");
                    }
                );
        }
    }
}
