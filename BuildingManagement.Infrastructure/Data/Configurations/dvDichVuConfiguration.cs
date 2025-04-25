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
    public class dvDichVuConfiguration : IEntityTypeConfiguration<dvDichVu>
    {
        public void Configure(EntityTypeBuilder<dvDichVu> builder)
        {
            builder.HasKey(a => a.MaDV);

            builder.HasMany(dv => dv.dvDichVuSuDungs)
                .WithOne(dv => dv.dvDichVu)
                .HasForeignKey(dv => dv.MaDV);
        }
    }
}
