﻿using BuildingManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Configurations
{
    public class dvDienConfiguration : IEntityTypeConfiguration<dvDien>
    {
        public void Configure(EntityTypeBuilder<dvDien> builder)
        {
            builder.HasKey(a => a.MaDien);
        }
    }
}
