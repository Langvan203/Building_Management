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
    public class dvHoaDonConfiguration : IEntityTypeConfiguration<dvHoaDon>
    {
        public void Configure(EntityTypeBuilder<dvHoaDon> builder)
        {
            builder.HasKey(a => a.MaHD);

            builder.HasMany(dv => dv.ptPhieuThus)
                .WithOne(dv => dv.dvHoaDon)
                .HasForeignKey(dv => dv.MaHD);
        }
    }
}
