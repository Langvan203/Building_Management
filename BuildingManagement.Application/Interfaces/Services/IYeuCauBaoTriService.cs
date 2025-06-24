using BuildingManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IYeuCauBaoTriService
    {
        Task<PagedResult<YeuCauSuaChuaDTO>> GetDSYeuCauSuaChua(int pageNumber, int pageSize = 10);
        Task<bool> DuyetYeuCau(int MaYC);
        Task<bool> TuChoiYeuCau(int MaYC);
        Task<bool> DanhDauHoanThanh(int MaYC);
        Task<bool> GiaoViecChoNhanVien(GiaoViecYeuCauChoNhanVien dto, string Name);
        //Task<YeuCauSuaChuaDTO> GetYeuCauById(int maYC);
        //Task<bool> CreateYeuCau(YeuCauSuaChuaDTO yeuCau);
        //Task<bool> UpdateYeuCau(YeuCauSuaChuaDTO yeuCau);
        //Task<bool> DeleteYeuCau(int maYC);
    }
}
