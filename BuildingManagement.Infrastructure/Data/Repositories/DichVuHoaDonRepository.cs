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

        public async Task<List<HoaDonDTO>> GetDSHoaDon()
        {
            var dsHoaDon = await _context.dvHoaDons.Include(x => x.dvDichVuSuDung).ThenInclude(x => x.dvDichVu).AsSplitQuery().ToListAsync();
            var hoaDonDtos = dsHoaDon.Select(x => new HoaDonDTO
            {
                MaHD = x.MaHD,
                MaMB = x.MaMB,
                MaDVSD = x.MaDVSD,
                DaThanhToan = x.DaThanhToan,
                IsThanhToan = x.IsThanhToan,
                TenDVSD = x.dvDichVuSuDung?.dvDichVu?.TenDV,
                //dichVuNuocs = x.dvDichVuNuocs.Select(n => new DichVuNuoc
                //{
                //    MaNuoc = n.MaNuoc,
                //    MaDM = n.MaDM,
                //    ChiSoDau = n.ChiSoDau,
                //    ChiSoCuoi = n.ChiSoCuoi,
                //    SoTieuThu = n.SoTieuThu,
                //    ThanhTien = n.ThanhTien
                //}).ToList(),
            }).ToList();

            //var dvSD = await _context.dvDichVuSuDungs.Include(x => x.dvDichVu).AsSplitQuery().ToListAsync();
            //var dvNuoc = dvSD.Select(x => x.dvDichVu).Where(x => x.MaLDV == 2).Select(x => x.MaDV).ToList();
            //if(dvNuoc.Count != 0)
            //{
            //    var dsNuocKhachHang = await _context.dvNuocs.Where(x => dvNuoc.Contains(x => x))
            //}    
            //var dvDien = dvSD.Select(x => x.dvDichVu).Where(x => x.MaLDV == 1).ToList();
            //var dsKhach = dvSD.Select(x => x.dvDichVu).Where(x => x.MaLDV != 1 && x.MaLDV != 2).ToList();
            return hoaDonDtos;
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
