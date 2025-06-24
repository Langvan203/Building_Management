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
    public class tnPhongBanConfiguration : IEntityTypeConfiguration<tnPhongBan>
    {
        public void Configure(EntityTypeBuilder<tnPhongBan> builder)
        {
            builder.HasKey(pb => pb.MaPB);

            builder.HasMany(pb => pb.tnNhanViens)
                .WithMany(pb => pb.tnPhongBans)
                .UsingEntity<Dictionary<string, object>>(
                "PhongBan_NhanVien",

                    pb => pb.HasOne<tnNhanVien>()
                    .WithMany()
                    .HasForeignKey("MaNV")
                    .OnDelete(DeleteBehavior.Cascade),

                    pb => pb.HasOne<tnPhongBan>()
                    .WithMany()
                    .HasForeignKey("MaPB")
                    .OnDelete(DeleteBehavior.Cascade),

                    pb =>
                    {
                        pb.HasKey("MaPB", "MaNV");
                    }
                );
        }
    }
}
