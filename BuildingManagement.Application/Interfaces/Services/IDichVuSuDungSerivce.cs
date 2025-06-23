using BuildingManagement.Application.DTOs;
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
        Task<PagedResult<GetDSYeuCauSuDung>> GetDSYeuCauSuDung(int pageNumber, DateTime ngayBatDau, DateTime ngayKetThuc, int pageSize = 15);
        Task<PagedResult<GetDSDangSuDung>> GetDSDangSuDung(int pageNumber, int pageSize = 15);
        Task<PagedResult<GetThongKeSuDung>> GetThongKeSuDung(int pageNumber, DateTime ngayBatDau, DateTime ngayKetThuc, int pageSize = 15);
        Task<CreateDichVuSuDungDto> CreateDichVuSuDung(CreateDichVuSuDungDto createDichVuSuDungDto, string Name);
        Task<bool> DuyetSangHoaDon(int MaDVSD,string Name);
        Task<byte[]> ExportThongKeToExcel(DateTime ngayBatDau, DateTime ngayKetThuc);
        Task<bool> DuyetYeuCau(int MaDVSD);
        Task<bool> TuChoiYeuCau(int MaDVSD);
        Task<bool> NgungSuDung(int MaDVSD);
        Task<bool> TiepTucSuDung(int MaDVSD);
    }
}
