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
    public class NKBTKeHoachBaoTriRepository : Repository<nkbtKeHoachBaoTri>, INKBTKeHoachBaoTriRepository
    {
        public NKBTKeHoachBaoTriRepository(BuildingManagementDbContext context) : base(context)
        {
        }
    }
    
}
