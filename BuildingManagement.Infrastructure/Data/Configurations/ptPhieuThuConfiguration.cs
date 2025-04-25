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
    public class ptPhieuThuConfiguration : IEntityTypeConfiguration<ptPhieuThu>
    {
        public void Configure(EntityTypeBuilder<ptPhieuThu> builder)
        {
            builder.HasKey(a => a.MaPT);


        }
    }
}
