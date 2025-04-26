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
    public class tnKhoiNhaConfiguration : IEntityTypeConfiguration<tnKhoiNha>
    {
        public void Configure(EntityTypeBuilder<tnKhoiNha> builder)
        {
            builder.HasKey(tn => tn.MaKN);


            builder.HasMany(tn => tn.tnTangLaus)
                .WithOne(tn => tn.tnKhoiNha)
                .HasForeignKey(tn => tn.MaKN);
        }
    }
}
