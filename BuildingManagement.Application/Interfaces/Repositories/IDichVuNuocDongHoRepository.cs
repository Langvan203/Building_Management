using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IDichVuNuocDongHoRepository : IRepository<dvNuocDongHo>
    {
        Task<PagedResult<DongHoDTO>> GetDSDongHoNuocPaged(int pageNumber, int pageSize = 15);
        Task<dvNuocDongHo> CheckDongHo(int MaDongHo);
        Task<bool> CheckThemDongHoNuoc(CreateDongHoDto dto);
    }
}
