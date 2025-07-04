﻿using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Response;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services
{
    public class DichVuHoaDonService : IDichVuHoaDonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DichVuHoaDonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CapNhatDanhSachHoaDonTrangThaiThanhToan(IEnumerable<dvHoaDon> hoaDOn, bool TrangThaiThanhToan)
        {
            foreach(var item in hoaDOn)
            {
                item.IsThanhToan = TrangThaiThanhToan;
                item.PhaiThu = 0;
                item.DaThanhToan = hoaDOn.Sum(x => x.PhaiThu);
                item.ConNo = 0;
                await _unitOfWork.HoaDons.UpdateAsync(item);
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CapNhatTrangThaiThanhToan(int MaHoaDon, bool TrangThaiThanhToan)
        {
            var hoaDon = await _unitOfWork.HoaDons.GetByIdAsync(MaHoaDon);
            if (hoaDon == null)
            {
                throw new KeyNotFoundException($"Hóa đơn với mã {MaHoaDon} không tồn tại.");
            }
            hoaDon.IsThanhToan = TrangThaiThanhToan;
            await _unitOfWork.HoaDons.UpdateAsync(hoaDon);
            await _unitOfWork.SaveChangesAsync();
            return true;

        }

        public async Task<PagedResult<GetDSHoaDon>> GetDSHoaDon(int pageNumber, DateTime NgayBatDau, DateTime NgayKetThuc, int pageSize = 15)
        {
            var dsHoaDon = await _unitOfWork.HoaDons.GetDSHoaDon(pageNumber, NgayBatDau,NgayKetThuc,pageSize);
            return dsHoaDon;
        }

        public async Task<PagedResult<GetDSHoaDon>> GetDSHoaDonByID(int id,int pageNumber, DateTime NgayBatDau, DateTime NgayKetThuc, int pageSize = 15)
        {
            var dsHoaDon = await _unitOfWork.HoaDons.GetDSHoaDonByID(id,pageNumber, NgayBatDau, NgayKetThuc, pageSize);
            return dsHoaDon;
        }

        public Task<IEnumerable<dvHoaDon>> GetDSHoaDonByMaKH(int MaKH)
        {
            var dsHoaDon = _unitOfWork.HoaDons.GetAllConditionAsync(x => x.MaKH == MaKH);
            return dsHoaDon;
        }

        public async Task<dvHoaDon> GetHoaDonByID(int MaHoaDon)
        {
            var hoaDon = await _unitOfWork.HoaDons.GetHoaDonByID(MaHoaDon);
            if (hoaDon == null)
            {
                throw new KeyNotFoundException($"Hóa đơn với mã {MaHoaDon} không tồn tại.");
            }
            return hoaDon;
        }

        public Task<IEnumerable<RevenueSummaryResponseDto>> GetRevenueSummariesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<RevenueSummaryOverview>> GetRevenueSummariesOverviewAsync()
        {
            throw new NotImplementedException();
        }
    }
}
