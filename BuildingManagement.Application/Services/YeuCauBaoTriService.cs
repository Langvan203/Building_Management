using BuildingManagement.Application.DTOs;
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
    public class YeuCauBaoTriService : IYeuCauBaoTriService
    {
        private readonly IUnitOfWork _unitOfWork;

        public YeuCauBaoTriService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DanhDauHoanThanh(int MaYC)
        {
            var yeuCau = await _unitOfWork.YeuCauBaoTris.GetFirstOrDefaultAsync(y => y.MaYC == MaYC);
            if (yeuCau == null)
            {
                throw new Exception("Yêu cầu sửa chữa không tồn tại.");
            }
            yeuCau.IdTrangThai = 4;
            await _unitOfWork.YeuCauBaoTris.UpdateAsync(yeuCau);
            await _unitOfWork.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DuyetYeuCau(int MaYC)
        {
            var yeuCau = await _unitOfWork.YeuCauBaoTris.GetFirstOrDefaultAsync(y => y.MaYC == MaYC);
            if (yeuCau == null)
            {
                throw new Exception("Yêu cầu sửa chữa không tồn tại.");
            }
            yeuCau.IdTrangThai = 2;
            await _unitOfWork.YeuCauBaoTris.UpdateAsync(yeuCau);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public Task<PagedResult<YeuCauSuaChuaDTO>> GetDSYeuCauSuaChua(int pageNumber, int pageSize = 10)
        {
            var dsYeuCau = _unitOfWork.YeuCauBaoTris.GetDSYeuCauSuaChua(pageNumber, pageSize);
            if (dsYeuCau == null)
            {
                throw new Exception("Không tìm thấy dữ liệu yêu cầu sửa chữa.");
            }
            return dsYeuCau;
        }

        public async Task<bool> GiaoViecChoNhanVien(GiaoViecYeuCauChoNhanVien dto, string Name)
        {
            var yeuCau = await _unitOfWork.YeuCauBaoTris.CheckYeuCauIncludeNhanVien(dto.MaYC);
            if (yeuCau == null)
            {
                throw new Exception("Yêu cầu sửa chữa không tồn tại.");
            }
            if(yeuCau.IdTrangThai == 3 || yeuCau.IdTrangThai == 2)
            {
                var oldNhanVien = yeuCau.tnNhanViens.ToList();
                if(oldNhanVien.Count != 0)
                {
                    oldNhanVien.Clear();
                }
                else
                {
                    yeuCau.tnNhanViens = new List<tnNhanVien>();
                }    
                var dsNhanVien = await _unitOfWork.NhanViens.GetAllConditionAsync(nv => dto.DanhSachNhanVien.Contains(nv.MaNV));
                yeuCau.tnNhanViens = dsNhanVien.ToList();
                yeuCau.NguoiSua = Name;
                await _unitOfWork.YeuCauBaoTris.UpdateAsync(yeuCau);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> TuChoiYeuCau(int MaYC)
        {
            var yeuCau = await _unitOfWork.YeuCauBaoTris.GetFirstOrDefaultAsync(y => y.MaYC == MaYC);
            if (yeuCau == null)
            {
                throw new Exception("Yêu cầu sửa chữa không tồn tại.");
            }
            yeuCau.IdTrangThai = 6;
            await _unitOfWork.YeuCauBaoTris.UpdateAsync(yeuCau);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
