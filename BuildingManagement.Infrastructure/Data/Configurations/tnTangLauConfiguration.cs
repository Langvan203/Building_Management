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
    public class tnTangLauConfiguration : IEntityTypeConfiguration<tnTangLau>
    {
        public void Configure(EntityTypeBuilder<tnTangLau> builder)
        {
            builder.HasKey(tn => tn.MaTL);

            builder.HasMany(tn => tn.tnMatBangs)
                .WithOne(tn => tn.tnTangLau)
                .HasForeignKey(tn => tn.MaTL);

            builder.HasMany(tn => tn.tnKhachHangs)
                .WithOne(tn => tn.tnTangLau)
                .HasForeignKey(tn => tn.MaTL)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(tn => tn.dvDichVuSuDungs)
                .WithOne(tn => tn.tnTangLau)
                .HasForeignKey(tn => tn.MaTL)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(tn => tn.dvHoaDons)
                .WithOne(tn => tn.tnTangLau)
                .HasForeignKey(tn => tn.MaTL)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
