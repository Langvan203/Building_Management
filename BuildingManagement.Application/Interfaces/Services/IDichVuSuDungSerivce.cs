using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IDichVuSuDungSerivce
    {
        Task<IEnumerable<DichVuSuDungDto>> GetDSDichVuSuDung();
        Task<IEnumerable<DichVuSuDungDto>> GetDSDichVuSuDungByMaKH(int MaKH);
        Task<DichVuSuDungDto> CreateDichVuSuDung(CreateDichVuSuDungDto dto, string name);
    }
}
