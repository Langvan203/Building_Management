using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface ILoaiMatBangService
    {
        Task<IEnumerable<LoaiMatBangDto>> GetDSLoaiMB();
        Task<LoaiMatBangDto> CreateNewLoaiMB(CreateNewLoaiMB dto, string Name);
        Task<bool> DeleteLoaiMB(int MaLMB);

    }
}
