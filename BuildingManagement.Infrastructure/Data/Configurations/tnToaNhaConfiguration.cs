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
    public class tnToaNhaConfiguration : IEntityTypeConfiguration<tnToaNha>
    {
        public void Configure(EntityTypeBuilder<tnToaNha> builder)
        {
            builder.HasKey(tn => tn.MaTN);

            builder.HasMany(tn => tn.tnKhoiNhas)
                .WithOne(tn => tn.tnToaNha)
                .HasForeignKey(tn => tn.MaTN)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(tn => tn.tnTangLaus)
                .WithOne(tn => tn.tnToaNha)
                .HasForeignKey(tn => tn.MaTN)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(tn => tn.tnMatBangs)
                .WithOne(tn => tn.tnToaNha)
                .HasForeignKey(tn => tn.MaTN)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
