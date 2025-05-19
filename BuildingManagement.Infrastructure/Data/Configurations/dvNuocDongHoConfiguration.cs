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
    public class dvNuocDongHoConfiguration : IEntityTypeConfiguration<dvNuocDongHo>
    {
        public void Configure(EntityTypeBuilder<dvNuocDongHo> builder)
        {
            builder.HasKey(dv => dv.MaDH);


            builder.HasOne(mb => mb.tnMatBang)
                .WithOne(mb => mb.dvNuocDongHo)
                .HasForeignKey<dvNuocDongHo>(mb => mb.MaMB);


            builder.HasMany(dv => dv.dvNuocs)
                .WithOne(dv => dv.dvNuocDongHo)
                .HasForeignKey(dv => dv.MaDH);

        }
    }
}
