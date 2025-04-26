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
    public class dvNuocDinhMucConfiguration : IEntityTypeConfiguration<dvNuocDinhMuc>
    {
        public void Configure(EntityTypeBuilder<dvNuocDinhMuc> builder)
        {
            builder.HasKey(dv => dv.MaDM);

            builder.HasMany(dv => dv.dvNuocs)
                .WithOne(dv => dv.dvNuocDinhMuc)
                .HasForeignKey(dv => dv.MaDM);
        }
    }
}
