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
    public class tnbtHeThongConfiguration : IEntityTypeConfiguration<tnbtHeThong>
    {
        public void Configure(EntityTypeBuilder<tnbtHeThong> builder)
        {
            builder.HasKey(ht => ht.MaHeThong);

            builder.HasMany(ht => ht.nkbtKeHoachBaoTris)
                .WithOne(ht => ht.tnbtHeThong)
                .HasForeignKey(ht => ht.MaHeThong);

            builder.HasMany(ht => ht.nkbtLichSuBaoTris)
                .WithOne(ht => ht.tnbtHeThong)
                .HasForeignKey(ht => ht.MaHeThong);

            builder.HasMany(ht => ht.tnycYeuCauSuaChua)
                .WithOne(ht => ht.tnbtHeThong)
                .HasForeignKey(ht => ht.MaHeThong);
        }
    }
}
