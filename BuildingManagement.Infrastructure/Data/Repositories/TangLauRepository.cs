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
    public class TangLauRepository : Repository<tnTangLau>, ITangLauRepository
    {
        public TangLauRepository(BuildingManagementDbContext context) : base(context)
        {
            
        }

        public Task<IEnumerable<tnTangLau>> GetTangLauByMaTN(int MaTN)
        {
            throw new NotImplementedException();
        }
    }
}
