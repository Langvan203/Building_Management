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
    public class dvNuocConfiguration : IEntityTypeConfiguration<dvNuoc>
    {
        public void Configure(EntityTypeBuilder<dvNuoc> builder)
        {
            builder.HasKey(dv => dv.MaNuoc);
        }
    }
}
