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
    public class dvDienDongHoConfiguration : IEntityTypeConfiguration<dvDienDongHo>
    {
        public void Configure(EntityTypeBuilder<dvDienDongHo> builder)
        {
            builder.HasKey(dv => dv.MaDH);

            builder.HasOne(mb => mb.tnMatBang)
                .WithOne(mb => mb.dvDienDongHo)
                .HasForeignKey<dvDienDongHo>(dv => dv.MaMB);

            builder.HasMany(dv => dv.dvDiens)
                .WithOne(dv => dv.dvDienDongHo)
                .HasForeignKey(dv => dv.MaDH);

            builder.HasOne(dv => dv.tnKhachHang)
                .WithOne(dv => dv.dvDienDongHo)
                .HasForeignKey<dvDienDongHo>(dv => dv.MaKH);
        }
    }
}
