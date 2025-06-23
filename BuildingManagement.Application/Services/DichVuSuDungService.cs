using AutoMapper;
using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Application.Services.Ultility;
using BuildingManagement.Domain.Entities;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BuildingManagement.Application.Services
{
    public class DichVuSuDungService : IDichVuSuDungSerivce
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuSuDungService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateDichVuSuDungDto> CreateDichVuSuDung(CreateDichVuSuDungDto createDichVuSuDungDto, string Name)
        {
            var checkDangKySuDung = await _unitOfWork.DichVuSuDungs.CheckDangKySuDung(createDichVuSuDungDto.MaKH, createDichVuSuDungDto.MaMB, createDichVuSuDungDto.MaDV, createDichVuSuDungDto.NgayBatDauTinhPhi, createDichVuSuDungDto.NgayKetThucTinhPhi);
            if(checkDangKySuDung == null)
            {
                var newSuDung = _mapper.Map<dvDichVuSuDung>(createDichVuSuDungDto);
                newSuDung.NguoiTao = Name;
                newSuDung.IsChuyenHoaDon = false;
                await _unitOfWork.DichVuSuDungs.AddAsync(newSuDung);
                await _unitOfWork.SaveChangesAsync();
                return createDichVuSuDungDto;
            }
            return null;
        }

        public async Task<bool> DuyetSangHoaDon(int MaDVSD, string Name)
        {
            var getDVSD = await _unitOfWork.DichVuSuDungs.CheckDichVuSuDungIncludeManyTable(MaDVSD);
            if(getDVSD != null)
            {
                getDVSD.IsChuyenHoaDon = true;
                getDVSD.UpdatedDate = DateTime.Now;

                var checkHoaDon = await _unitOfWork.HoaDons.CheckTonTaiDichVuSuDung(MaDVSD);
                if(!checkHoaDon)
                {
                    var newHoaDon = new dvHoaDon
                    {
                        ThueVAT = getDVSD.dvDichVu.TyLeVAT,
                        ThueBVMT = getDVSD.dvDichVu.TyLeBVMT,
                        TienTruocVAT = getDVSD.dvDichVu.DonGia,
                        DaThanhToan = 0,
                        ConNo = getDVSD.ThanhTien,
                        TienBVMT = getDVSD.TienBVMT,
                        TienVAT = getDVSD.TienVAT,
                        PhaiThu = getDVSD.ThanhTien,
                        IsThanhToan = false,
                        GhiChu = "Duyệt sang hóa đơn từ dịch vụ " + getDVSD.dvDichVu.TenDV + " của khách hàng " + (getDVSD.tnKhachHang.IsCaNhan ? getDVSD.tnKhachHang.HoTen : getDVSD.tnKhachHang.CtyTen),
                        MaDVSD = getDVSD.MaDVSD,
                        NguoiTao = Name,
                        MaKN = getDVSD.MaKN,
                        MaTL = getDVSD.MaTL,
                        MaTN = getDVSD.MaTN,
                        MaKH = getDVSD.MaKH,
                        MaMB = getDVSD.MaMB,
                    };
                    await _unitOfWork.HoaDons.AddAsync(newHoaDon);
                }
                else
                {
                    return false;
                }    
                await _unitOfWork.DichVuSuDungs.UpdateAsync(getDVSD);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DuyetYeuCau(int MaDVSD)
        {
            var checkYeuCau = await _unitOfWork.DichVuSuDungs.CheckDichVuSuDung(MaDVSD);
            if(checkYeuCau != null)
            {
                checkYeuCau.IsDuyet = 1;
                checkYeuCau.TrangThaiSuDung = true;
                await _unitOfWork.DichVuSuDungs.UpdateAsync(checkYeuCau);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<byte[]> ExportThongKeToExcel(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            var dsThongKeSuDung = await _unitOfWork.DichVuSuDungs.GetThongKeSuDung(1, ngayBatDau, ngayKetThuc);
            var dsThongKe = dsThongKeSuDung.Data;
            var columnConfigs = new List<ExcelColumnConfig>
            {
                new ExcelColumnConfig("Mã DVSD", "MaDVSD", width: 15),
                new ExcelColumnConfig("Tên khách hàng", "TenKH", width: 25),
                new ExcelColumnConfig("Tên dịch vụ", "TenDV", width: 20),
                new ExcelColumnConfig("Ngày bắt đầu", "NgayBatDauSuDung", "dd/MM/yyyy", 15, ExcelHorizontalAlignment.Center),
                new ExcelColumnConfig("Ngày đến hạn", "NgayDenHanThanhToan", "dd/MM/yyyy", 15, ExcelHorizontalAlignment.Center),
                new ExcelColumnConfig("Tiền VAT", "TienVAT", "#,##0", 15, ExcelHorizontalAlignment.Right),
                new ExcelColumnConfig("Tiền BVMT", "TienBVMT", "#,##0", 15, ExcelHorizontalAlignment.Right),
                new ExcelColumnConfig("Thành tiền", "ThanhTien", "#,##0", 15, ExcelHorizontalAlignment.Right),
                new ExcelColumnConfig("Trạng thái HĐ", obj =>
                {
                    var item = (GetThongKeSuDung)obj;
                    return item.IsDuyetHoaDon ? "Đã duyệt" : "Chưa duyệt";
                })
            };
            return ExcelExportHelper.ExportToExcel<GetThongKeSuDung>(
            dsThongKe,
            columnConfigs,
            "Thống kê sử dụng",
            $"THỐNG KÊ SỬ DỤNG DỊCH VỤ ({ngayBatDau:dd/MM/yyyy} - {ngayKetThuc:dd/MM/yyyy})"
    );
        }

        public async Task<PagedResult<GetDSDangSuDung>> GetDSDangSuDung(int pageNumber, int pageSize = 15)
        {
            var dsDangSuDung = await _unitOfWork.DichVuSuDungs.GetDSDangSuDung(pageNumber,pageSize);
            return dsDangSuDung;
        }

        public async Task<PagedResult<GetDSYeuCauSuDung>> GetDSYeuCauSuDung(int pageNumber, DateTime ngayBatDau, DateTime ngayKetThuc, int pageSize = 15)
        {
            var dsYeuCauSuDung = await _unitOfWork.DichVuSuDungs.GetDSYeuCauSuDung(pageNumber, ngayBatDau, ngayKetThuc, pageSize);
            return dsYeuCauSuDung;
        }

        public async Task<PagedResult<GetThongKeSuDung>> GetThongKeSuDung(int pageNumber, DateTime ngayBatDau, DateTime ngayKetThuc, int pageSize = 15)
        {
            var thongKeSuDung = await _unitOfWork.DichVuSuDungs.GetThongKeSuDung(pageNumber, ngayBatDau, ngayKetThuc, pageSize);
            return thongKeSuDung;
        }

        public async Task<bool> NgungSuDung(int MaDVSD)
        {
            var dvDichVuSuDung = await _unitOfWork.DichVuSuDungs.CheckDichVuSuDung(MaDVSD);
            if(dvDichVuSuDung != null)
            {
                dvDichVuSuDung.TrangThaiSuDung = false;
                dvDichVuSuDung.UpdatedDate = DateTime.Now;
                await _unitOfWork.DichVuSuDungs.UpdateAsync(dvDichVuSuDung);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> TiepTucSuDung(int MaDVSD)
        {
            var dvDichVuSuDung = await _unitOfWork.DichVuSuDungs.CheckDichVuSuDung(MaDVSD);
            if (dvDichVuSuDung != null)
            {
                dvDichVuSuDung.TrangThaiSuDung = true;
                dvDichVuSuDung.UpdatedDate = DateTime.Now;
                await _unitOfWork.DichVuSuDungs.UpdateAsync(dvDichVuSuDung);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> TuChoiYeuCau(int MaDVSD)
        {
            var checkYeuCau = await _unitOfWork.DichVuSuDungs.CheckDichVuSuDung(MaDVSD);
            if (checkYeuCau != null)
            {
                checkYeuCau.IsDuyet = -1;
                await _unitOfWork.DichVuSuDungs.UpdateAsync(checkYeuCau);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
