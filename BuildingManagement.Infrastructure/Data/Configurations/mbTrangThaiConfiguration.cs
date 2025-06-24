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
    public class mbTrangThaiConfiguration : IEntityTypeConfiguration<mbTrangThai>
    {
        public void Configure(EntityTypeBuilder<mbTrangThai> builder)
        {
            builder.HasKey(tt => tt.MaTrangThai);

            builder.HasMany(x => x.tnMatBangs)
                .WithOne(x => x.mbTrangThai)
                .HasForeignKey(x => x.MaTrangThai);
        }
    }
}
