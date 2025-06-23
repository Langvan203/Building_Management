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
    public class nkbtTrangThaiConfiguration : IEntityTypeConfiguration<nkbtTrangThai>
    {
        public void Configure(EntityTypeBuilder<nkbtTrangThai> builder)
        {
            builder.HasKey(tt => tt.MaTrangThai);

            builder.HasMany(x => x.nkbtKeHoachBaoTris)
                .WithOne(x => x.nkbtTrangThai)
                .HasForeignKey(x => x.MaTrangThai)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.nkbtChiTietBaoTris)
                .WithOne(x => x.nkbtTrangThai)
                .HasForeignKey(x => x.MaTrangThai)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
