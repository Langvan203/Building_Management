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
    public class PhongBanRepository : Repository<tnPhongBan>, IPhongBanRepository
    {
        public PhongBanRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public Task<bool> AddPhongBan(tnPhongBan phongBan)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePhongBan(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<tnPhongBan>> GetAllPhongBan()
        {
            throw new NotImplementedException();
        }

        public Task<tnPhongBan> GetPhongBanById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePhongBan(tnPhongBan phongBan)
        {
            throw new NotImplementedException();
        }
    }
   
}
