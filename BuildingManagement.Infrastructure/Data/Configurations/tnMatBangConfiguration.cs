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
    public class tnMatBangConfiguration : IEntityTypeConfiguration<tnMatBang>
    {
        public void Configure(EntityTypeBuilder<tnMatBang> builder)
        {
            builder.HasKey(mb => mb.MaMB);

            builder.HasMany(mb => mb.tnbtHeThongs)
                .WithOne(mb => mb.tnMatBang)
                .HasForeignKey(mb => mb.MaMB);

            builder.HasOne(mb => mb.mbTrangThai)
                .WithOne(mb => mb.tnMatBang)
                .HasForeignKey<mbTrangThai>(tt => tt.MaTrangThai);

            builder.HasMany(mb => mb.dvDichVuSuDungs)
                .WithOne(mb => mb.tnMatBang)
                .HasForeignKey(mb => mb.MaMB);

            builder.HasMany(mb => mb.dvgxTheXes)
                .WithOne(mb => mb.tnMatBang)
                .HasForeignKey(mb => mb.MaMB);

        }
    }
}
