using BuildingManagement.Application.DTOs;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IYeuCauBaoTriRepository : IRepository<tnycYeuCauSuaChua>
    {
        Task<PagedResult<YeuCauSuaChuaDTO>> GetDSYeuCauSuaChua(int pageNumber, int pageSize = 10);
        Task<tnycYeuCauSuaChua> CheckYeuCauIncludeNhanVien(int MaYC);
    }
}
