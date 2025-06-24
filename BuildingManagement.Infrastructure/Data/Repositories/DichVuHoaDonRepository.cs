using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Response;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Domain.Ultility;
using BuildingManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class DichVuHoaDonRepository : Repository<dvHoaDon>, IDichVuHoaDonRepository
    {
        public DichVuHoaDonRepository(BuildingManagementDbContext context) : base(context)
        {
        }

        public Task<bool> CheckTonTaiDichVuSuDung(int MaDVSD)
        {
            var exists = _context.dvHoaDons.AnyAsync(x => x.MaDVSD == MaDVSD);
            return exists;
        }


        public async Task<PagedResult<GetDSHoaDon>> GetDSHoaDon(int pageNumber, DateTime NgayBatDau, DateTime NgayKetThuc, int pageSize = 15)
        {
            // Lấy tất cả dữ liệu trước khi group by
            var allHoaDon = await _context.dvHoaDons
                .Include(x => x.tnMatBang)
                .Include(x => x.tnToaNha)
                .Include(x => x.tnKhoiNha)
                .Include(x => x.tnTangLau)
                .Include(x => x.tnKhachHang)
                .Include(x => x.dvDichVuSuDung)
                .ThenInclude(x => x.dvDichVu)
                .Where(x => x.CreatedDate >= NgayBatDau && x.CreatedDate <= NgayKetThuc)
                .ToListAsync();

            // Group by MaKH và tạo cấu trúc dữ liệu mới
            var groupedHoaDon = allHoaDon
                .GroupBy(x => x.MaKH)
                .Select(g => new GetDSHoaDon
                {
                    // Lấy thông tin từ hóa đơn đầu tiên trong group (vì thông tin khách hàng giống nhau)
                    MaKH = (int)g.Key,
                    MaTN = (int)g.First().MaTN,
                    MaKN = (int)g.First().MaKN,
                    MaTL = (int)g.First().MaTL,
                    MaMB = (int)g.First().MaMB,
                    TenTN = g.First().tnToaNha.TenTN,
                    NganHangThanhToan = g.First().tnToaNha.NganHangThanhToan,
                    SoTaiKhoan = g.First().tnToaNha.SoTaiKhoan,
                    TenKN = g.First().tnKhoiNha.TenKN,
                    TenTL = g.First().tnTangLau.TenTL,
                    MaVT = g.First().tnMatBang.MaVT,
                    MaHD = g.First().MaHD, // Lấy MaHD từ hóa đơn đầu tiên trong group
                    TenKhachHang = g.First().tnKhachHang.IsCaNhan ? g.First().tnKhachHang.HoTen : g.First().tnKhachHang.CtyTen,
                    acqId = g.First().tnToaNha.acqId,
                    TenTaiKhoan = g.First().tnToaNha.TenTaiKhoan,
                    EmailKhachHang = g.First().tnKhachHang.Email,

                    // Tính tổng phải thu của tất cả hóa đơn trong group
                    PhaiThu = g.Sum(x => x.PhaiThu),

                    // Kiểm tra tất cả hóa đơn đã thanh toán hay chưa
                    IsThanhToan = g.All(x => x.IsThanhToan),

                    // Lấy ngày thanh toán gần nhất hoặc ngày đến hạn gần nhất
                    NgayThanhToan = g.Max(x => x.NgayDenHan),

                    // Tạo danh sách chi tiết hóa đơn
                    HoaDonDetails = g.Select(x => new HoaDonDetail
                    {
                        MaHD = x.MaHD, // Thêm MaHD để phân biệt các hóa đơn
                        MaDVSD = x.MaDVSD,
                        TenDichVu = x.dvDichVuSuDung.dvDichVu.TenDV,
                        TienVAT = x.dvDichVuSuDung.TienVAT,
                        TienBVMT = x.dvDichVuSuDung.TienBVMT,
                        ThanhTien = x.dvDichVuSuDung.ThanhTien,
                        ThueVAT = x.dvDichVuSuDung.dvDichVu.TyLeVAT,
                        ThueBVMT = x.dvDichVuSuDung.dvDichVu.TyLeBVMT,
                    }).ToList()
                })
                .OrderBy(x => x.MaKH); // Sắp xếp theo MaKH

            var totalCount = groupedHoaDon.Count();
            var items = groupedHoaDon.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalPage = (int)Math.Ceiling((double)totalCount / pageSize);

            var pagedResult = new PagedResult<GetDSHoaDon>
            {
                Data = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPage
            };

            return pagedResult;
        }

        public Task<dvHoaDon> GetHoaDonByID(int MaHoaDon)
        {
            var hoaDon = _context.dvHoaDons.Where(x => x.MaHD == MaHoaDon).Include(x => x.tnKhachHang).FirstOrDefaultAsync();
            return hoaDon;
        }

        public async Task<IEnumerable<RevenueSummaryResponseDto>> GetRevenueSummariesAsync()
        {
            IEnumerable<RevenueSummaryResponseDto> revenueSummaries;
            try
            {
                var hoaDonData = await _context.dvHoaDons.Where(x => x.IsThanhToan == true).ToListAsync();
                var totalElectricRevenue = hoaDonData.Where(x => x.dvDichVuSuDung.dvDichVu.MaDV ==1).Sum(x => x.DaThanhToan);
                var totalWaterRevenue = hoaDonData.Where(x => x.dvDichVuSuDung.dvDichVu.MaDV == 2).Sum(x => x.DaThanhToan);
                var totalServiceRevenue = hoaDonData.Where(x => x.dvDichVuSuDung.dvDichVu.MaDV == 3).Sum(x => x.DaThanhToan);
                var totalParkingRevenue = hoaDonData.Where(x => x.dvDichVuSuDung.dvDichVu.MaDV == 4).Sum(x => x.DaThanhToan);
                var totalManagementFeeRevenue = hoaDonData.Where(x => x.dvDichVuSuDung.dvDichVu.MaDV == 5).Sum(x => x.DaThanhToan);
                var totalOtherRevenue = hoaDonData.Where(x => x.dvDichVuSuDung.dvDichVu.MaDV == 6).Sum(x => x.DaThanhToan);
                revenueSummaries = new List<RevenueSummaryResponseDto>
                {
                    new RevenueSummaryResponseDto
                    {
                        ServiceName = "Tiền điện",
                        TotalRevenue = totalElectricRevenue
                    },
                    new RevenueSummaryResponseDto
                    {
                        ServiceName = "Tiền nước",
                        TotalRevenue = totalWaterRevenue
                    },
                    new RevenueSummaryResponseDto
                    {
                        ServiceName = "Tiền dịch vụ",
                        TotalRevenue = totalServiceRevenue
                    },
                    new RevenueSummaryResponseDto
                    {
                        ServiceName = "Tiền gửi xe",
                        TotalRevenue = totalParkingRevenue
                    },
                    new RevenueSummaryResponseDto
                    {
                        ServiceName = "Phí quản lý",
                        TotalRevenue = totalManagementFeeRevenue
                    },
                    new RevenueSummaryResponseDto
                    {
                        ServiceName = "Khác",
                        TotalRevenue = totalOtherRevenue
                    }
                };
                return revenueSummaries;
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw;
            }
        }

        public async Task<List<RevenueSummaryOverview>> GetRevenueSummariesOverviewAsync()
        {
            int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            List<RevenueSummaryOverview> revenueSummaries = new List<RevenueSummaryOverview>();
            foreach (var item in months)
            {
                var totalRevenue = await _context.dvHoaDons
                    .Where(x => x.CreatedDate.Month == item && x.IsThanhToan == true)
                    .SumAsync(x => x.DaThanhToan);
                var newRevenueOverView = new RevenueSummaryOverview
                {
                    Month = ConvertNumberToString.ConvertMonth(item),
                    TotalRevenue = totalRevenue
                };
                revenueSummaries.Add(newRevenueOverView);
            }    
            return revenueSummaries;
        }

        
    }
    
}
