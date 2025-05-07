using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IDichVuGuiXeLoaiXeService
    {
        Task<IEnumerable<DichVuGuiXeLoaiXeDto>> GetDSLoaiXe();
        Task<DichVuGuiXeLoaiXeDto> CreateNewLoaiXe(CreateDichVuGuiXeLoaiXeDto dto, string name);
    }
}
