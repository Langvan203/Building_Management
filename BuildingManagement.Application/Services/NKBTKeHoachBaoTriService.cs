using AutoMapper;
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
    public class NKBTKeHoachBaoTriService : INKBTKeHoachBaoTriService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NKBTKeHoachBaoTriService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> BatDauKeHoach(int MaKeHoach, string Name)
        {
            var checkKeHoach = await _unitOfWork.KeHoachBaoTris.CheckKeHoach(MaKeHoach);
            if (checkKeHoach == null)
            {
                return false;
            }
            checkKeHoach.MaTrangThai = 2;
            checkKeHoach.NguoiSua = Name;
            await _unitOfWork.KeHoachBaoTris.UpdateAsync(checkKeHoach);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<CreateKeHoachBaoTriDto> CreateKeHoachBaoTri(CreateKeHoachBaoTriDto dto, string name)
        {
            var checkBaoTri = await _unitOfWork.KeHoachBaoTris.GetFirstOrDefaultAsync(x => x.MaHeThong == dto.MaHeThong && x.NgayBaoTri == dto.NgayBaoTri && x.TenKeHoach == dto.TenKeHoach);
            if(checkBaoTri != null)
            {
                return null;
            }
            var newKeHoachBaoTri = _mapper.Map<nkbtKeHoachBaoTri>(dto);
            newKeHoachBaoTri.NguoiTao = name;
            if(dto.DanhSachNhanVien.Count != 0)
            {
                var dsNhanVien = await _unitOfWork.NhanViens.GetAllConditionAsync(x => dto.DanhSachNhanVien.Contains(x.MaNV));
                newKeHoachBaoTri.tnNhanViens = dsNhanVien.ToList();
            }
            await _unitOfWork.KeHoachBaoTris.AddAsync(newKeHoachBaoTri);
            await _unitOfWork.SaveChangesAsync();
            if (dto.ChiTietBaoTris.Count != 0)
            {
                await _unitOfWork.ChiTietBaoTris.CreateChiTietBaoTri(newKeHoachBaoTri.MaKeHoach, dto.ChiTietBaoTris);
            }
            return dto;

        }

        public async Task<PagedResult<KeHoachBaoTriDto>> GetDSKeHoachBaoTri(int pageNumber, int pageSize = 15)
        {
            var dsKeHoach = await _unitOfWork.KeHoachBaoTris.GetDSKeHoachBaoTri(pageNumber, pageSize);
            return dsKeHoach;
        }

        public async Task<bool> GiaoViecChoNhanVien(GiaoViecChoNhanVien dto, string Name)
        {
            var keHoach = await _unitOfWork.KeHoachBaoTris.CheckKeHoach(dto.MaKeHoach);
            if (keHoach == null)
            {
                return false;
            }

            var oldNhanVien = keHoach.tnNhanViens.Select(x => x.MaNV).ToList();
            if (oldNhanVien.Count == 0)
            {
                keHoach.tnNhanViens = new List<tnNhanVien>();
            }
            else
            {
                keHoach.tnNhanViens.Clear();
            }
            var dsNhanVien = await _unitOfWork.NhanViens.GetAllConditionAsync(x => dto.MaNV.Contains(x.MaNV));
            if (dsNhanVien.Count() == 0)
            {
                return false;
            }
            keHoach.tnNhanViens = dsNhanVien.ToList();
            keHoach.NguoiSua = Name;
            await _unitOfWork.KeHoachBaoTris.UpdateAsync(keHoach);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HoanThanhKeHoach(int MaKeHoach, string Name)
        {
            var checkKeHoach = await _unitOfWork.KeHoachBaoTris.CheckKeHoach(MaKeHoach);
            if (checkKeHoach == null)
            {
                return false;
            }
            checkKeHoach.MaTrangThai = 3;
            checkKeHoach.NguoiSua = Name;
            await _unitOfWork.KeHoachBaoTris.UpdateAsync(checkKeHoach);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HuyKeHoach(int MaKeHoach, string Name)
        {
            var checkKeHoach = await _unitOfWork.KeHoachBaoTris.CheckKeHoach(MaKeHoach);
            if (checkKeHoach == null)
            {
                return false;
            }
            checkKeHoach.MaTrangThai = 4;
            checkKeHoach.NguoiSua = Name;
            await _unitOfWork.KeHoachBaoTris.UpdateAsync(checkKeHoach);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> XoaKeHoach(int MaKeHoach)
        {
            var checkKeHoach = await _unitOfWork.KeHoachBaoTris.CheckKeHoach(MaKeHoach);
            if (checkKeHoach == null)
            {
                return false;
            }
            await _unitOfWork.KeHoachBaoTris.DeleteAsync(checkKeHoach);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
