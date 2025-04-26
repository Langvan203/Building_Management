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
    public class dvgxLoaiXeConfiguration : IEntityTypeConfiguration<dvgxLoaiXe>
    {
        public void Configure(EntityTypeBuilder<dvgxLoaiXe> builder)
        {
            builder.HasKey(lx => lx.MaLX);

            builder.HasMany(tx => tx.dvgxTheXes)
                .WithOne(tx => tx.dvgxLoaiXe)
                .HasForeignKey(tx => tx.MaLX);
        }
    }
}
