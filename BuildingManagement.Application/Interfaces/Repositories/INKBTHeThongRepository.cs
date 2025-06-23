using BuildingManagement.Application.DTOs;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface INKBTHeThongRepository : IRepository<tnbtHeThong>
    {
        Task<PagedResult<HeThongDTO>> GetDSHeThong(int pageNumber, int pageSize = 15);
        Task<tnbtHeThong> CheckHeThong(int MaHeThong);
    }
}
