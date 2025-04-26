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
    public class dvDienDinhMucConfiguration : IEntityTypeConfiguration<dvDienDinhMuc>
    {
        public void Configure(EntityTypeBuilder<dvDienDinhMuc> builder)
        {
            builder.HasKey(dv => dv.MaDM);

            builder.HasMany(dv => dv.dvDiens)
                .WithOne(dv => dv.dvDienDinhMuc)
                .HasForeignKey(dv => dv.MaDM);
        }
    }
}
