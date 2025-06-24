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
    public interface IDichVuSuDungRepository : IRepository<dvDichVuSuDung>
    {
        Task<PagedResult<GetDSYeuCauSuDung>> GetDSYeuCauSuDung(int pageNumber, DateTime ngayBatDau, DateTime ngayKetThuc, int pageSize = 15);
        Task<PagedResult<GetDSDangSuDung>> GetDSDangSuDung(int pageNumber, int pageSize = 15);
        Task<PagedResult<GetThongKeSuDung>> GetThongKeSuDung(int pageNumber, DateTime ngayBatDau, DateTime ngayKetThuc, int pageSize = 15);
        Task<dvDichVuSuDung> CheckDichVuSuDung(int MaDVSD);
        Task<dvDichVuSuDung> CheckDichVuSuDungIncludeManyTable(int MaDVSD);
        Task<dvDichVuSuDung> CheckDangKySuDung(int MaKH, int MaMB, int MaDV, DateTime ngayBatDau, DateTime ngayKetThuc);
    }
}
