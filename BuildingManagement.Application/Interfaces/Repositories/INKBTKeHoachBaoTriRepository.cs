using BuildingManagement.Application.DTOs;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface INKBTKeHoachBaoTriRepository : IRepository<nkbtKeHoachBaoTri>
    {
        Task<PagedResult<KeHoachBaoTriDto>> GetDSKeHoachBaoTri(int pageNumber, int pageSize = 15);
        Task<nkbtKeHoachBaoTri> CheckKeHoach(int MaKeHoach);
    }
}
