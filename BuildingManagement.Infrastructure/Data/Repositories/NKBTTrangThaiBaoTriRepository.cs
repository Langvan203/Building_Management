using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class NKBTTrangThaiBaoTriRepository : Repository<nkbtTrangThai>, INKBTTrangThaiBaoTriRepository
    {
        public NKBTTrangThaiBaoTriRepository(BuildingManagementDbContext context) : base(context)
        {
        }
    }
    
}
