using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IDichVuGuiXeTheXeService
    {
        Task<IEnumerable<DichVuGuiXeTheXeDto>> GetDSTheXe();
        Task<IEnumerable<DichVuGuiXeTheXeDto>> GetDSTheXeByMaKH(int MaKH);
        Task<IEnumerable<DichVuGuiXeTheXeDto>> GetTheXeByMaMB(int MaTX);
        Task<IEnumerable<DichVuGuiXeTheXeDto>> GetTheXeByMaLX(int MaLX);
        Task<DichVuGuiXeTheXeDto> CreateNewTheXe(CreateDichVuGuiXeTheXeDto dto, string name);
    }
}
