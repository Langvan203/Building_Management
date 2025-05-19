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
    public class tnycTrangThaiConfiguration : IEntityTypeConfiguration<tnycTrangThai>
    {
        public void Configure(EntityTypeBuilder<tnycTrangThai> builder)
        {
            builder.HasKey(c => c.IdTrangThai);

            builder.HasMany(c => c.tnycYeuCaus)
                .WithOne(c => c.tnycTrangThai)
                .HasForeignKey(c => c.IdTrangThai)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

