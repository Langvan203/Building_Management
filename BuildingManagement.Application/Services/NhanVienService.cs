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
