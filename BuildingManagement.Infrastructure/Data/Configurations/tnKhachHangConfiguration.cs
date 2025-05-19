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
    public class tnKhachHangConfiguration : IEntityTypeConfiguration<tnKhachHang>
    {
        public void Configure(EntityTypeBuilder<tnKhachHang> builder)
        {
            builder.HasKey(kh => kh.MaKH);


            builder.HasMany(yc => yc.tnycYeuCauSuaChuas)
                .WithOne(yc => yc.tnKhachHang)
                .HasForeignKey(kh => kh.MaKH);

            builder.HasMany(mb => mb.tnMatBangs)
                .WithOne(mb => mb.tnKhachHang)
                .HasForeignKey(mb => mb.MaKH);

            builder.HasMany(tx => tx.dvgxTheXes)
                .WithOne(tx => tx.tnKhachHang)
                .HasForeignKey(tx => tx.MaKH);

            builder.HasMany(dv => dv.dvDichVuSuDungs)
                .WithOne(dv => dv.tnKhachHang)
                .HasForeignKey(dv => dv.MaKH);

            //builder.HasOne(dh => dh.dvDienDongHo)
            //    .WithOne(dh => dh.tnKhachHang)
            //    .HasForeignKey();

            builder.HasMany(dv => dv.dvHoaDons)
                .WithOne(tn => tn.tnKhachHang)
                .HasForeignKey(tn => tn.MaKH)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(dh => dh.dvDienDongHo)
                .WithOne(dh => dh.tnKhachHang)
                .HasForeignKey(dh => dh.MaKH)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(dh => dh.dvNuocDongHo)
                .WithOne(dh => dh.tnKhachHang)
                .HasForeignKey(dh => dh.MaKH)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
