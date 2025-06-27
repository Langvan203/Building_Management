using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Response;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class ToaNhaRepository : Repository<tnToaNha>, IToaNhaRepository
    {
        public ToaNhaRepository(BuildingManagementDbContext context) : base(context)
        {
            
        }

        public async Task<BuildingDataOverView> BuildingsData(DateTime from, DateTime to)
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            var lastMonth = currentMonth == 1 ? 12 : currentMonth - 1;
            var lastMonthYear = currentMonth == 1 ? currentYear - 1 : currentYear;

            var tnData = await _context.tnToaNhas.Where(x => x.CreatedDate >= from && x.CreatedDate <= to).Include(x => x.tnMatBangs).Include(x => x.tnKhachHangs).Include(x => x.dvDichVus).Include(x => x.tnycYeuCauSuaChuas).Include(x => x.dvHoaDons).AsSplitQuery().ToListAsync();
            var totalBuilding = _context.tnToaNhas.Count();
            var totalUnit = _context.tnMatBangs.Count();
            var occupiedUnits = _context.tnMatBangs.Where(x => x.MaTrangThai == 1).Count();
            var maintanceIssues = _context.tnycYeuCauSuaChuas.Count();
            var occupanceRate = (decimal)occupiedUnits / totalUnit * 100;

            var totalRevenue = tnData.SelectMany(x => x.dvHoaDons.Where(x => x.CreatedDate.Month == currentMonth && x.CreatedDate.Year == currentYear)).Sum(x => x.DaThanhToan);
            var totalRevenueLastMonth = tnData.SelectMany(x => x.dvHoaDons.Where(x => x.CreatedDate.Month == lastMonth && x.CreatedDate.Year == lastMonthYear)).Sum(x => x.DaThanhToan);
            var revenueGrowth = totalRevenueLastMonth == 0 ? 100 : (decimal)(totalRevenue - totalRevenueLastMonth) / totalRevenueLastMonth * 100;

            var newCustomers = tnData.SelectMany(x => x.tnKhachHangs.Where(x => x.CreatedDate.Month == currentMonth && x.CreatedDate.Year == currentYear)).Count();
            var newCustomersLastMonth = tnData.SelectMany(x => x.tnKhachHangs.Where(x => x.CreatedDate.Month == lastMonth && x.CreatedDate.Year == lastMonthYear)).Count();
            var newCustomersGrowth = newCustomersLastMonth == 0 ? 100 : (decimal)(newCustomers - newCustomersLastMonth) / newCustomersLastMonth * 100;

            var totalRequestThisMonth = tnData.SelectMany(x => x.tnycYeuCauSuaChuas.Where(x => x.CreatedDate.Month == currentMonth && x.CreatedDate.Year == currentYear)).Count();
            var totalRequestLastMonth = tnData.SelectMany(x => x.tnycYeuCauSuaChuas.Where(x => x.CreatedDate.Month == lastMonth && x.CreatedDate.Year == lastMonthYear)).Count();
            
            var totalRequestCompareLastMonth = totalRequestLastMonth == 0 ? 100 : (decimal)(totalRequestThisMonth - totalRequestLastMonth) / totalRequestLastMonth * 100;

            var totalCompletedRequestThisMonth = tnData.SelectMany(x => x.tnycYeuCauSuaChuas.Where(x => x.IdTrangThai == 4 && x.CreatedDate.Month == currentMonth && x.CreatedDate.Year == currentYear)).Count();
            var totalCompletedRequestLastMonth = tnData.SelectMany(x => x.tnycYeuCauSuaChuas.Where(x => x.IdTrangThai == 4 && x.CreatedDate.Month == lastMonth && x.CreatedDate.Year == lastMonthYear)).Count();

            var CompletedRequestThisMonthRate = totalRequestThisMonth == 0 ? 0 : (decimal)totalCompletedRequestThisMonth / totalRequestThisMonth * 100;
            var CompletedRequestLastMonthRate = totalRequestLastMonth == 0 ? 0 : (decimal)totalCompletedRequestLastMonth / totalRequestLastMonth * 100;

            var totalRequestGrowth = CompletedRequestThisMonthRate - CompletedRequestLastMonthRate;


            List<BuildingDetails> buildings = new List<BuildingDetails>();
            List<occupancyBuilding> occupancyBuildings = new List<occupancyBuilding>();
            foreach (var building in tnData)
            {
                BuildingDetails newBD = new BuildingDetails();
                occupancyBuilding occupancy = new occupancyBuilding();
                newBD.BuildingName = building.TenTN;
                newBD.Units = building.tnMatBangs.Count();
                newBD.Occupied = building.tnMatBangs.Where(x => x.MaTrangThai == 1).ToList().Count();
                newBD.vacant = building.tnMatBangs.Where(x => x.MaTrangThai == 1).ToList().Count();
                buildings.Add(newBD);
                occupancy.BuildingName = building.TenTN;
                occupancy.value = newBD.Units != 0 ? (decimal)newBD.Occupied / newBD.Units * 100 : 0;
                occupancyBuildings.Add(occupancy);
            }
            BuildingDataOverView buildingDataOverView = new BuildingDataOverView
            {
                totalBuildings = totalBuilding,
                totalUnits = totalUnit,
                occupiedUnits = occupiedUnits,
                occupancyRate = occupanceRate,
                maintanceIssues = maintanceIssues,
                buildingDetails = buildings,
                occupancyBuildings = occupancyBuildings,
                totalRevenue = totalRevenue,
                totalRevenueGroth = revenueGrowth,
                NewCustomers = newCustomers,
                NewCustomersGroth = newCustomersGrowth,
                totalRequest = totalRequestThisMonth,
                totalRequestGroth = totalRequestCompareLastMonth,
                TotalCompletedRequest = CompletedRequestThisMonthRate,
                TotalCompletedRequestGroth = totalRequestGrowth

            };
            return buildingDataOverView;
        }

        public async Task<FinnancesData> GetFinnancesDataAsync(DateTime from, DateTime to)
        {
            try
            {
                var tnData = await _context.tnToaNhas.Include(x => x.dvHoaDons).ThenInclude(x => x.tnKhachHang).Include(x => x.tnMatBangs).Include(x => x.dvDichVus).Include(x => x.dvDichVuSuDungs).ThenInclude(x => x.dvDichVu).AsSplitQuery().ToListAsync();
                // chi phi thang hien tai
                var paidMonthlyRevenue = tnData.SelectMany(x => x.dvDichVuSuDungs.Where(x => x.CreatedDate.Month == DateTime.Now.Month && x.CreatedDate.Year == DateTime.Now.Year)).Sum(x => x.ThanhTien);
                // chi phi thang truoc
                var lastpaidMonthlyRevenue = tnData.SelectMany(x => x.dvDichVuSuDungs.Where(x => x.CreatedDate.Month == DateTime.Now.Month - 1 && x.CreatedDate.Year == DateTime.Now.Year)).Sum(x => x.ThanhTien);
                // tong doanh thu thang
                var totalRevenue = tnData.SelectMany(x => x.dvHoaDons.Where(x => x.CreatedDate.Month == DateTime.Now.Month && x.CreatedDate.Year == DateTime.Now.Year)).Sum(x => x.DaThanhToan);
                // tong doanh thu thang truoc
                var lastTotalRevenue = tnData.SelectMany(x => x.dvHoaDons.Where(x => x.CreatedDate.Month == DateTime.Now.Month - 1 && x.CreatedDate.Year == DateTime.Now.Year)).Sum(x => x.DaThanhToan);
                // chua thanh toan theo thang
                var outstandings = tnData.SelectMany(x => x.dvHoaDons.Where(x => x.CreatedDate.Month == DateTime.Now.Month && x.CreatedDate.Year == DateTime.Now.Year)).Sum(x => x.ConNo);
                // khoan phai thu thu
                var needPaid = totalRevenue + outstandings;
                var collectionRate = needPaid == 0 ? 0 : (decimal)totalRevenue / needPaid * 100;
                var revenueGrowth = lastTotalRevenue == 0 ? 100 : (decimal)(totalRevenue - lastTotalRevenue) / lastTotalRevenue * 100;
                var revenuePaid = lastpaidMonthlyRevenue == 0 ? 100 : (decimal)(paidMonthlyRevenue - lastpaidMonthlyRevenue) / lastpaidMonthlyRevenue * 100;
                //thong ke theo thang
                var allMonths = Enumerable.Range(1, 12)
                .Select(month => new RevenueByMonth
                {
                    Month = month.ToString(),
                    Revenue = 0
                }).ToList();
                var totalRevuenueByYear = tnData.SelectMany(x => x.dvHoaDons.Where(x => x.CreatedDate.Year == DateTime.Now.Year))
                    .GroupBy(x => x.CreatedDate.Month).Select(x => new RevenueByMonth
                    {
                        Month = x.Key.ToString(),
                        Revenue = (int)x.Sum(x => x.DaThanhToan)
                    }).ToList();
                foreach (var item in totalRevuenueByYear)
                {
                    var monthItem = allMonths.FirstOrDefault(m => m.Month == item.Month);
                    if (monthItem != null)
                    {
                        monthItem.Revenue = item.Revenue;
                    }
                }
                var totalByYear = totalRevuenueByYear.Sum(x => x.Revenue);

                var totalExpenseByYear = tnData.SelectMany(x => x.dvDichVuSuDungs.Where(x => x.CreatedDate.Year == DateTime.Now.Year))
                    .GroupBy(x => new { x.CreatedDate.Month }).Select(x => new ExpenseCategory
                    {
                        Name = x.First().dvDichVu.TenDV,
                        Value = (int)x.Sum(x => x.ThanhTien)
                    }).ToList();

                var overduePayments = tnData.SelectMany(x => x.dvHoaDons.Where(x => x.ConNo > 0 && x.CreatedDate < DateTime.Now)).Skip(0).Take(5).Select(x => new OverduePayment
                {
                    Id = x.MaHD.ToString(),
                    Apartment = x.tnMatBang.MaVT,
                    Resident = x.tnKhachHang.HoTen,
                    Amount = (int)x.ConNo,
                    DueDate = x.CreatedDate,
                    DaysOverdue = (int)(DateTime.Now - x.CreatedDate).TotalDays
                }).ToList();
                var finnancesData = new FinnancesData
                {
                    TotalRevenue = totalByYear,
                    MonthlyRevenue = totalRevenue,
                    OutstandingPayments = outstandings,
                    CollectionRate = collectionRate,
                    RevenueGrowth = revenueGrowth,
                    revenuePaid = revenuePaid,
                    MonthlyExpenses = paidMonthlyRevenue,
                    RevenueByMonth = allMonths,
                    ExpenseCategories = totalExpenseByYear,
                    OverduePayments = overduePayments
                };
                return finnancesData;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
        //public async Task<FinnancesData> GetFinnancesDataAsync(DateTime from, DateTime to)
        //{
        //    // 1. Tách biến ngày tháng để tái sử dụng
        //    var currentMonth = DateTime.Now.Month;
        //    var currentYear = DateTime.Now.Year;
        //    var lastMonth = currentMonth == 1 ? 12 : currentMonth - 1;
        //    var lastMonthYear = currentMonth == 1 ? currentYear - 1 : currentYear;

        //    // 2. Thực hiện các truy vấn tuần tự thay vì song song

        //    // Chi phí tháng hiện tại
        //    var paidMonthlyRevenue = await _context.dvDichVuSuDungs
        //        .Where(x => x.CreatedDate.Month == currentMonth && x.CreatedDate.Year == currentYear)
        //        .SumAsync(x => x.ThanhTien);

        //    // Chi phí tháng trước
        //    var lastpaidMonthlyRevenue = await _context.dvDichVuSuDungs
        //        .Where(x => x.CreatedDate.Month == lastMonth && x.CreatedDate.Year == lastMonthYear)
        //        .SumAsync(x => x.ThanhTien);

        //    // Dữ liệu doanh thu tháng hiện tại
        //    var currentMonthRevenueData = await _context.dvHoaDons
        //        .Where(x => x.CreatedDate.Month == currentMonth && x.CreatedDate.Year == currentYear)
        //        .Select(x => new { x.DaThanhToan, x.ConNo })
        //        .ToListAsync();

        //    var totalRevenue = currentMonthRevenueData.Sum(x => x.DaThanhToan);
        //    var outstandings = currentMonthRevenueData.Sum(x => x.ConNo);

        //    // Dữ liệu doanh thu tháng trước
        //    var lastTotalRevenue = await _context.dvHoaDons
        //        .Where(x => x.CreatedDate.Month == lastMonth && x.CreatedDate.Year == lastMonthYear)
        //        .SumAsync(x => x.DaThanhToan);

        //    // 3. Tính toán các chỉ số
        //    var needPaid = totalRevenue + outstandings;
        //    var collectionRate = needPaid == 0 ? 0 : (decimal)totalRevenue / needPaid * 100;
        //    var revenueGrowth = lastTotalRevenue == 0 ? 100 : (decimal)(totalRevenue - lastTotalRevenue) / lastTotalRevenue * 100;
        //    var revenuePaid = lastpaidMonthlyRevenue == 0 ? 100 : (decimal)(paidMonthlyRevenue - lastpaidMonthlyRevenue) / lastpaidMonthlyRevenue * 100;

        //    // 4. Lấy doanh thu theo tháng
        //    var allMonths = Enumerable.Range(1, 12)
        //        .Select(month => new RevenueByMonth { month = month.ToString(), revenue = 0 })
        //        .ToList();

        //    var totalRevuenueByYear = await _context.dvHoaDons
        //        .Where(x => x.CreatedDate.Year == currentYear)
        //        .GroupBy(x => x.CreatedDate.Month)
        //        .Select(x => new RevenueByMonth
        //        {
        //            month = x.Key.ToString(),
        //            revenue = (int)x.Sum(x => x.DaThanhToan)
        //        })
        //        .ToListAsync();

        //    foreach (var item in totalRevuenueByYear)
        //    {
        //        var monthItem = allMonths.FirstOrDefault(m => m.month == item.month);
        //        if (monthItem != null)
        //        {
        //            monthItem.revenue = item.revenue;
        //        }
        //    }

        //    var totalByYear = allMonths.Sum(x => x.revenue);

        //    // 5. Lấy chi phí theo danh mục
        //    var totalExpenseByYear = await _context.dvDichVuSuDungs
        //        .Where(x => x.CreatedDate.Year == currentYear)
        //        .Join(_context.dvDichVus,
        //              dv => dv.MaDV,
        //              dvInfo => dvInfo.MaDV,
        //              (dv, dvInfo) => new { dv.CreatedDate, dv.ThanhTien, TenDV = dvInfo.TenDV })
        //        .GroupBy(x => new { x.TenDV })
        //        .Select(x => new expenseCategory
        //        {
        //            Name = x.Key.TenDV,
        //            value = (int)x.Sum(x => x.ThanhTien)
        //        })
        //        .ToListAsync();

        //    // 6. Lấy thanh toán quá hạn - với phân trang
        //    const int pageSize = 100;

        //    // Fix for the error CS1061: 'dvHoaDon' does not contain a definition for 'MaMatBang'  
        //    // The property 'MaMatBang' does not exist in the 'dvHoaDon' class based on the provided type signature.  
        //    // To resolve this, we need to replace 'hd.MaMatBang' with a valid property or relationship that links 'dvHoaDon' to 'tnMatBang'.  

        //    // Updated code:  
        //    var overduePayments = await _context.dvHoaDons
        //        .Where(x => x.ConNo > 0 && x.CreatedDate < DateTime.Now)
        //        .OrderByDescending(x => x.ConNo)
        //        .Take(pageSize)
        //        .Join(_context.tnMatBangs,
        //              hd => hd.MaMB, // Replace 'hd.MaMatBang' with 'hd.MaMB', which is a valid property in 'dvHoaDon'.  
        //              mb => mb.MaMB,
        //              (hd, mb) => new { HoaDon = hd, MatBang = mb })
        //        .Join(_context.tnKhachHangs,
        //              joined => joined.HoaDon.MaKH,
        //              kh => kh.MaKH,
        //              (joined, kh) => new overduePayment
        //              {
        //                  id = joined.HoaDon.MaHD.ToString(),
        //                  apartment = joined.MatBang.MaVT,
        //                  resident = kh.HoTen,
        //                  amount = (int)joined.HoaDon.ConNo,
        //                  dueDate = joined.HoaDon.CreatedDate,
        //                  daysOverdue = (int)(DateTime.Now - joined.HoaDon.CreatedDate).TotalDays
        //              })
        //        .ToListAsync();


        //    // 7. Trả về kết quả
        //    var finnancesData = new FinnancesData
        //    {
        //        totalRevenue = totalByYear,
        //        monthlyRevenue = totalRevenue,
        //        outstandingPayments = outstandings,
        //        collectionRate = collectionRate,
        //        revenueGrowth = revenueGrowth,
        //        revenuePaid = revenuePaid,
        //        monthlyExpenses = paidMonthlyRevenue,
        //        revenueByMonth = allMonths,
        //        expenseCategories = totalExpenseByYear,
        //        overduePayments = overduePayments
        //    };

        //    return finnancesData;
        //}
        public async Task<IEnumerable<OccupancyRate>> GetOccupancyRateAsync()
        {
            IEnumerable<OccupancyRate> occupancyRates = await _context.tnToaNhas
                .Include(x => x.tnMatBangs)
                .Select(x => new OccupancyRate
                {
                    buildingName = x.TenTN,
                    Rate = (decimal)x.tnMatBangs.Where(x => x.MaTrangThai == 2).Count() / x.tnMatBangs.Count() * 100
                }).ToListAsync();
            return occupancyRates;
        }

        public async Task<OverViewData> GetOverViewData(int year)
        {
            var overViewData = await _context.tnToaNhas.Include(x => x.dvDichVus)
                                                .Include(x => x.dvHoaDons)
                                                .ThenInclude(x => x.ptPhieuThus)
                                                .Include(x => x.tnMatBangs)
                                                .Include(x => x.tnKhachHangs)
                                                .Include(x => x.dvDichVuSuDungs)
                                                .Include(x => x.tnycYeuCauSuaChuas)
                                                .Where(x => x.CreatedDate.Year == year).AsSplitQuery().ToListAsync();

            var allQuarters = Enumerable.Range(1, 4)
                .Select(quarter => new RevenueByQuarter
                {
                    Quarter = quarter.ToString(),
                    Revenue = 0
                }).ToList();

            var revenueByQuarter = overViewData.SelectMany(x => x.dvHoaDons).GroupBy(x => new { Quater = ((x.CreatedDate.Month-1)/3)+1, x.CreatedDate.Year }).Select(x => new RevenueByQuarter
            {
                Quarter = x.Key.Quater.ToString(),
                Revenue = (int)x.Sum(x => x.DaThanhToan)
            }).ToList();

            foreach (var item in revenueByQuarter)
            {
                var quarterItem = allQuarters.FirstOrDefault(m => m.Quarter == item.Quarter);
                if (quarterItem != null)
                {
                    quarterItem.Revenue = item.Revenue;
                }
            }

            var buildingStatus = overViewData.SelectMany(x => x.tnMatBangs).GroupBy(x => x.MaTN).Select(x => new BuildingStatus
            {
                Name = x.First().tnToaNha.TenTN,
                Occupancy = (decimal)x.Where(x => x.MaTrangThai == 2).Count() / x.Count() * 100,
                Maintaince = x.Select(x => x.tnycYeuCauSuaChuas).Count(),
            }).ToList();

            var servicesDistrubution = overViewData.SelectMany(x => x.dvDichVuSuDungs).GroupBy(x => x.MaDV).Select(x => new ServiceDistribution
            {
                Name = x.Key == 1 ? "Điện" : x.Key == 2 ? "Nước" : x.Key == 3 ? "Internet" : "Khác",
                Value = x.Count()
            }).ToList();

            var recentransactions = overViewData.SelectMany(x => x.dvHoaDons).SelectMany(x => x.ptPhieuThus).Skip(0).Take(5).OrderByDescending(x => x.CreatedDate).Select(x => new RecentTransactions
            {
                ID = x.MaHD.ToString(),
                Resident = x.dvHoaDon.tnKhachHang.HoTen,
                Apartment = x.dvHoaDon.tnMatBang.MaVT,
                Amount = x.SoTien,
                type = x.HinhThucThanhToan,
                date = x.CreatedDate
            }).ToList();
            var issueByPriority = overViewData.SelectMany(x => x.tnycYeuCauSuaChuas).GroupBy(x => x.MucDoYeuCau).Select(x => new IssueByPriority
            {
                Name = x.Key == 1 ? "Cao" : x.Key == 2 ? "Thấp" : "TB",
                Value = x.Count()
            }).ToList();
            var oveviewData = new OverViewData
            {
                RevenueByQuarter = allQuarters,
                BuildingStatus = buildingStatus,
                ServiceDistribution = servicesDistrubution,
                RecentTransactions = recentransactions,
                IssueByPriority = issueByPriority,
            };

            return oveviewData;
        }

        public async Task<ServicesData> GetServicesData(DateTime from, DateTime to)
        {
            var svData =await _context.tnToaNhas.Include(x => x.tnbtHeThongs)
                                                    .ThenInclude(x => x.tnycYeuCauSuaChua)
                                                    .ThenInclude(x => x.tnMatBang)
                                                    .ThenInclude(x => x.tnKhachHang)
                                                 .Include(x => x.tnbtHeThongs)
                                                    .ThenInclude(x => x.tnycYeuCauSuaChua).ThenInclude(x => x.tnycTrangThai)
                                            .Include(x => x.tnNhanViens)
                                                .ThenInclude(x => x.nvDanhGias).AsSplitQuery().ToListAsync();
            var totalRequest = _context.tnycYeuCauSuaChuas.Where(x => x.CreatedDate >= from && x.CreatedDate <= to).Count();
            var pendingRequest = _context.tnycYeuCauSuaChuas.Where(x => x.IdTrangThai != 4 && x.CreatedDate >= from && x.CreatedDate <= to).Count();
            var completedRequest = _context.tnycYeuCauSuaChuas.Where(x => x.IdTrangThai == 4 && x.CreatedDate >= from && x.CreatedDate <= to).Count();
            var completionRequest = totalRequest == 0 ? 100 : (decimal)completedRequest / totalRequest * 100;
            var totalPoint = svData.SelectMany(x => x.tnNhanViens).SelectMany(x => x.nvDanhGias).Sum(x => x.DiemDanhGia);
            var totalEvaluate = svData.SelectMany(x => x.tnNhanViens).SelectMany(x => x.nvDanhGias).Count();
            var satisfactionRate = (totalPoint != 0 && totalEvaluate != 0) ? (decimal)totalPoint / totalEvaluate * 100 : 0;

            var requestByCategory = svData.SelectMany(x => x.tnbtHeThongs)
                .GroupBy(x => new { x.TenHeThong })
                .Select(x => new RequestByCategory
                {
                    Name = x.Key.TenHeThong,
                    Total = x.Select(x => x.tnycYeuCauSuaChua).Count(),
                    Peding = x.First().tnycYeuCauSuaChua.Where(x => x.IdTrangThai == 1).Count(),
                    Completed = x.First().tnycYeuCauSuaChua.Where(x => x.IdTrangThai == 4).Count()
                }).ToList();


            // thong ke theo thang
            var allMonths = Enumerable.Range(1, 12)
                .Select(month => new RequestByMonth
                {
                    Month = month.ToString(),
                    Requests = 0
                }).ToList();

            var requestByMonth = svData.SelectMany(x => x.tnbtHeThongs).SelectMany(x => x.tnycYeuCauSuaChua)
                .GroupBy(x => new { x.CreatedDate.Month, x.CreatedDate.Year })
                .Select(x => new RequestByMonth
                {
                    Month = x.Key.Month.ToString(),
                    Requests = x.Count()
                }).ToList();
            foreach (var item in requestByMonth)
            {
                var monthItem = allMonths.FirstOrDefault(m => m.Month == item.Month);
                if (monthItem != null)
                {
                    monthItem.Requests = item.Requests;
                }
            }
            var recentRequest = svData.SelectMany(x => x.tnbtHeThongs).SelectMany(x => x.tnycYeuCauSuaChua).OrderBy(x => x.CreatedDate).Skip(0).Take(5).Select(x => new RecentRequest
            {
                Id = x.MaYC.ToString(),
                Title = x.tnMatBang.tnKhachHang.HoTen + "YC",
                Apartment = x.tnMatBang.MaVT,
                Status = x.tnycTrangThai.TenTrangThai,
                Date = x.CreatedDate
            }).ToList();
            var servicesData = new ServicesData
            {
                TotalRequest = totalRequest,
                PendingRequest = pendingRequest,
                CompletedRequest = completedRequest,
                CompletionRate = completionRequest,
                SatisfactionRate = satisfactionRate,
                RequestsByCategory = requestByCategory,
                RequestsByMonth = allMonths,
                RecentRequests = recentRequest
            };
            return servicesData;

        }

        public async Task<List<ToaNhaDto>> GetToaNhaDtoAsync()
        {
            var dsToaNha = await _context.tnToaNhas.Include(x => x.tnKhachHangs).Include(x => x.tnMatBangs).AsSplitQuery().ToListAsync();
            var toaNhaDto = dsToaNha.Select(x => new ToaNhaDto
            {
                 Id = x.MaTN,
                 Name = x.TenTN,
                 Address = x.DiaChi,
                 SoTangNoi = x.SoTangNoi,
                 SoTangHam = x.SoTangHam,
                 DienTichXayDung = x.DienTichXayDung,
                 Status = x.TrangThaiToaNha == 1 ? "Hoạt động" : "Không hoạt động",
                 ConstructionYear = x.CreatedDate.Year,
                 TongDienTichSan = x.TongDienTichSan,
                 TongDienTichChoThueNET = x.TongDienTichChoThueNET,
                 TongDienTichChoThueGross = x.TongDienTichChoThueGross,
                 NganHangThanhToan = x.NganHangThanhToan,
                 SoTaiKhoan = x.SoTaiKhoan,
                 NoiDungChuyenKhoan = x.NoiDungChuyenKhoan,
            }).ToList();
            return toaNhaDto;
        }

        public async Task<SummaryTotalBuildingResponseDtoWithCompare> SummaryTotalBuildingAsync()
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            var dataToaNha = await _context.tnToaNhas.Include(x => x.tnMatBangs).Include(x => x.dvDichVus).Include(x => x.tnKhachHangs).Include(x => x.dvHoaDons).ToListAsync();

            var totalData = dataToaNha.Select(x => new SummaryTotalBuildingResponseDto
            {
                TotalPremises = x.tnMatBangs.Count(),
                TotalServices = x.dvDichVus.Count(),
                TotalResidents = x.tnKhachHangs.Count(),
                TotalMonthlyRevenue = x.dvHoaDons.Sum(x => x.DaThanhToan),
                OccupancyRate = (decimal)x.tnMatBangs.Where(x => x.MaTrangThai == 2).Count() / x.tnMatBangs.Count() * 100,
                TotalBuilding = dataToaNha.Count(),
                TotalOccupied = x.tnMatBangs.Where(x => x.MaTrangThai == 2).Count(),
            }).FirstOrDefault();

            var totalDataLastMonth = await _context.tnToaNhas.Where(x => x.CreatedDate.Month == month-1 && x.CreatedDate.Year == year)
                                            .Include(x => x.tnMatBangs.Where(x => x.CreatedDate.Month == month-1 && x.CreatedDate.Year == year))
                                            .Include(x => x.dvDichVus.Where(x => x.CreatedDate.Month == month-1 && x.CreatedDate.Year == year))
                                            .Include(x => x.tnKhachHangs.Where(x => x.CreatedDate.Month == month-1 && x.CreatedDate.Year == year))
                                            .Include(x => x.dvHoaDons.Where(x => x.CreatedDate.Month == month-1 && x.CreatedDate.Year == year)).ToListAsync();
            if(totalDataLastMonth.Count != 0)
            {
                var dataLastMonth = totalDataLastMonth.Select(x => new SummaryTotalBuildingResponseDto
                {
                    TotalPremises = x.tnMatBangs.Count(),
                    TotalServices = x.dvDichVus.Count(),
                    TotalResidents = x.tnKhachHangs.Count(),
                    TotalMonthlyRevenue = x.dvHoaDons.Sum(x => x.DaThanhToan),
                    OccupancyRate = (decimal)x.tnMatBangs.Where(x => x.MaTrangThai == 2).Count() / x.tnMatBangs.Count() * 100,
                    TotalBuilding = totalDataLastMonth.Count(),
                }).FirstOrDefault();

                var compareWithLastMonth = new CompareWithLastMonth
                {
                    LastMonthTotalBuilding = totalData.TotalBuilding - dataLastMonth.TotalBuilding,
                    LastMonthTotalPremises = totalData.TotalPremises - dataLastMonth.TotalPremises,
                    LastMonthTotalResidents = totalData.TotalResidents - dataLastMonth.TotalResidents,
                    RateLastMonthRevenue = dataLastMonth.TotalMonthlyRevenue != 0 ? (decimal)(totalData.TotalMonthlyRevenue - dataLastMonth.TotalMonthlyRevenue) / dataLastMonth.TotalMonthlyRevenue * 100 : 0,
                    RateLastMonthOccupancy = (decimal)(totalData.OccupancyRate - dataLastMonth.OccupancyRate) / dataLastMonth.OccupancyRate * 100,
                    LastMonthTotalServices = totalData.TotalServices - dataLastMonth.TotalServices,
                };
                return new SummaryTotalBuildingResponseDtoWithCompare
                {
                    CurrentMonth = totalData,
                    CompareWithLastMonth = compareWithLastMonth
                };
            }    
            return new SummaryTotalBuildingResponseDtoWithCompare
            {
                CurrentMonth = totalData,
                CompareWithLastMonth = null
            };


        }
    }
}
