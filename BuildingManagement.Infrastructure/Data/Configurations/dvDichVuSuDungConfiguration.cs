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
    public class dvDichVuSuDungConfiguration : IEntityTypeConfiguration<dvDichVuSuDung>
    {
        public void Configure(EntityTypeBuilder<dvDichVuSuDung> builder)
        {
            builder.HasKey(a => a.MaDVSD);

            builder.HasMany(dv => dv.dvHoaDons)
                .WithOne(dv => dv.dvDichVuSuDung)
                .HasForeignKey(dv => dv.MaDVSD);

        }
    }
}
