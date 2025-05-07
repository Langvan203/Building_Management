using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IDichVuGuiXeTheXeRepository : IRepository<dvgxTheXe>
    {
        Task<IEnumerable<DichVuGuiXeTheXeDto>> GetDSTheXeByMaKH(int MaKH);
        Task<IEnumerable<DichVuGuiXeTheXeDto>> GetTheXeByMaLX(int MaLX);
        Task<IEnumerable<DichVuGuiXeTheXeDto>> GetTheXeByMaMB(int MaMB);
    }
}
