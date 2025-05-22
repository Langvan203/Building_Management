using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IMatBangService
    {
        Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTL(int MaTL, int MaTN);
        Task<IEnumerable<MatBangDto>> GetDSMatBangByMaKH(int MaKH);
        Task<IEnumerable<MatBangDto>> GetDSMatBangByMaLMB(int MaLMB, int MaTN);
        Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTT(int MaTT, int MaTN);
        Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTN(int MaTN);
        Task<MatBangDto> CreateMatBang(CreateMatBangDto dto, string name);
        Task<bool> RemoveMatBang(int MaMB);
        Task<MatBangDto> UpdateMatBang(UpdateThongTinCoBanMatBangDto dto, string name);
        Task<MatBangDto> BanGiaoMatBang(int MaMB, int MaKH);
        Task<List<DanhSachMatBangDTO>> GetDSMatBang();

    }
}
