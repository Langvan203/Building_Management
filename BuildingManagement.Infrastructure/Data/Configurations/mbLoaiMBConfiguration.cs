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
    public class mbLoaiMBConfiguration : IEntityTypeConfiguration<mbLoaiMB>
    {
        public void Configure(EntityTypeBuilder<mbLoaiMB> builder)
        {
            builder.HasKey(mb => mb.MaLMB);

            builder.HasMany(mb => mb.tnMatBangs)
                .WithOne(mb => mb.mbLoaiMB)
                .HasForeignKey(mb => mb.MaLMB);
        }
    }
}
