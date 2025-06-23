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
    public class nkbtKeHoachBaoTriConfiguration : IEntityTypeConfiguration<nkbtKeHoachBaoTri>
    {
        public void Configure(EntityTypeBuilder<nkbtKeHoachBaoTri> builder)
        {
            builder.HasKey(nk => nk.MaKeHoach);

            builder.HasMany(nk => nk.nkbtChiTietBaoTris)
                .WithOne(nk => nk.nkbtKeHoachBaoTri)
                .HasForeignKey(nk => nk.MaKeHoach);

            builder.HasMany(nk => nk.tnNhanViens)
                .WithMany(nk => nk.nkbtKeHoachBaoTris)
                .UsingEntity<Dictionary<string, object>>("BaoTri_NhanVien",
                    nk => nk.HasOne<tnNhanVien>()
                    .WithMany()
                    .HasForeignKey("MaNV")
                    .OnDelete(DeleteBehavior.Cascade),

                    nk => nk.HasOne<nkbtKeHoachBaoTri>()
                    .WithMany()
                    .HasForeignKey("MaKeHoach")
                    .OnDelete(DeleteBehavior.Cascade),

                    nk =>
                    {
                        nk.HasKey("MaNV", "MaKeHoach");
                    }
                );

            builder.HasMany(nk => nk.nkbtLichSuBaoTris)
                .WithOne(nk => nk.nkbtKeHoachBaoTri)
                .HasForeignKey(nk => nk.MaKeHoach)
                .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
