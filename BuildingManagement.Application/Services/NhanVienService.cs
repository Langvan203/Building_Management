using AutoMapper;
using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request.AuthDto;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Ultility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services
{
    public class NhanVienService : INhanVienService
    {
        private readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        public NhanVienService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateNhanVienDto> CreateNewNhanVien(CreateNhanVienDto dto, string tennv)
        {
            var checkNV = await _unitOfWork.NhanViens.GetFirstOrDefaultAsync(nv => nv.Email == dto.Email);
            if(checkNV == null)
            {
                var passwordHash = HashPassWord.HashPassword(dto.Password);
                var newNV = _mapper.Map<tnNhanVien>(dto);
                newNV.NguoiTao = tennv;
                newNV.PasswordHash = passwordHash;
                await _unitOfWork.NhanViens.AddAsync(newNV);
                await _unitOfWork.SaveChangesAsync();
                return dto;
            }
            else
            {
                throw new Exception("Email đã tồn tại");
            }
        }



        public async Task<List<GetDSNhanVienDto>> GetDSNhanVien()
        {
            var dsNhanVien = await _unitOfWork.NhanViens.GetDSNhanVien();
            return dsNhanVien;
        }

        public Task<bool> RemoveNhanVien(int MaNV)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ThemNhanVienPhongBan(int manv, int MaPB)
        {
            var checkNVInPB = await _unitOfWork.NhanViens.CheckNVInPhongBan(manv, MaPB);
            if (checkNVInPB != null)
            {
                var pb = await _unitOfWork.PhongBans.GetByIdAsync(MaPB);
                if (pb != null)
                {
                    checkNVInPB.tnPhongBans.Add(pb);
                    await _unitOfWork.NhanViens.UpdateAsync(checkNVInPB);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new Exception("Phòng ban không tồn tại");
                }
            }
            return false;
        }

        public Task<bool> ThemNhanVienToToaNha(int manv, int MaTN)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePhongBanNhanVien(List<int> dsPhongBan, int maNV)
        {
            var getNV = await _unitOfWork.NhanViens.GetNhanVienInPhongBan(maNV);
            if(getNV == null)
            {
                throw new Exception("Nhân viên không tồn tại");
            }
            var dspb = await _unitOfWork.PhongBans.GetAllAsync();
            // lấy ra tất cả ds phòng ban hiện có
            var pbID = dspb.Select(x => x.MaPB).ToList();
            // lọc ra những danh sách phòng ban mà trong danh sách gửi đến có
            var dsPhongBanValid = dsPhongBan.Where(x => pbID.Contains(x)).ToList();
            var dsPBNew = await _unitOfWork.PhongBans.GetAllConditionAsync(x => dsPhongBanValid.Contains(x.MaPB));
            getNV.tnPhongBans = dsPBNew.ToList();
            await _unitOfWork.NhanViens.UpdateAsync(getNV);
            await _unitOfWork.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateRoleNhanVien(List<int> dsRole, int maNV)
        {
            var getNV = await _unitOfWork.NhanViens.GetNhanVienRoles(maNV);
            if (getNV == null)
            {
                throw new Exception("Nhân viên không tồn tại");
            }
            var dspb = await _unitOfWork.Roles.GetAllAsync();
            // lấy ra tất cả ds role hiện có
            var pbID = dspb.Select(x => x.RoleID).ToList();
            // lọc ra những danh sách role mà trong danh sách gửi đến có
            var dsRoleValid = dsRole.Where(x => pbID.Contains(x)).ToList();
            var dsRoleNew = await _unitOfWork.Roles.GetAllConditionAsync(x => dsRoleValid.Contains(x.RoleID));
            getNV.Roles = dsRoleNew.ToList();
            await _unitOfWork.NhanViens.UpdateAsync(getNV);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateThongTinNhanVien(UpdateThongTinNhanVien dto,string tennv)
        {
            var nv = await _unitOfWork.NhanViens.GetNhanVienInPhongBan(dto.MaNV);
            if(nv!= null)
            {
                nv.UserName = dto.TenDangNhap;
                nv.TenNV = dto.HoTen;
                nv.Email = dto.Email;
                nv.NgaySinh = dto.NgaySinh;
                nv.SDT = dto.SoDienThoai;
                nv.DiaChiThuongTru = dto.DiaChi;
                nv.NguoiSua = tennv;
                await _unitOfWork.NhanViens.UpdateAsync(nv);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateToaNhaNhanVien(List<int> dsToaNha, int maNV)
        {
            var getNV = await _unitOfWork.NhanViens.GetNhanVienInToaNha(maNV);
            if (getNV == null)
            {
                throw new Exception("Nhân viên không tồn tại");
            }
            var dstn = await _unitOfWork.ToaNhas.GetAllAsync();
            // lấy ra tất cả ds tòa nhà hiện có
            var pbID = dstn.Select(x => x.MaTN).ToList();
            // lọc ra những danh sách tòa nhà mà trong danh sách gửi đến có
            var dsTNValid = dsToaNha.Where(x => pbID.Contains(x)).ToList();
            var dsTNNew = await _unitOfWork.ToaNhas.GetAllConditionAsync(x => dsTNValid.Contains(x.MaTN));
            getNV.tnToaNhas = dsTNNew.ToList();
            await _unitOfWork.NhanViens.UpdateAsync(getNV);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public Task<bool> XoaNhanVienPhongBan(int manv, int MaPB)
        {
            throw new NotImplementedException();
        }

        public Task<bool> XoaNhanVienToaNha(int manv, int MaTN)
        {
            throw new NotImplementedException();
        }
    }
}
