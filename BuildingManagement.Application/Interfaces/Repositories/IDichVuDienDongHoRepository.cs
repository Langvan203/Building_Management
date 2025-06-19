using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IDichVuDienDongHoRepository : IRepository<dvDienDongHo>
    {
        Task<PagedResult<DongHoDTO>> GetDSDongHoDienPaged(int pageNumber, int pageSize = 15);
        Task<dvDienDongHo> CheckDongHo(int MaDongHo);
        Task<bool> CheckThemDongHoDien(CreateDongHoDto dto);
    }
}
