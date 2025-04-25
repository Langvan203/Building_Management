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
    public class dvLoaiDVConfiguration : IEntityTypeConfiguration<dvLoaiDV>
    {
        public void Configure(EntityTypeBuilder<dvLoaiDV> builder)
        {
            builder.HasKey(a => a.MaLDV);

            builder.HasMany(dv => dv.dvDichVus)
                .WithOne(dv => dv.dvLoaiDV)
                .HasForeignKey(dv => dv.MaLDV);
        }
    }
}
