using AutoMapper;
using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request;
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
    public class DichVuDienDinhMucService : IDichVuDienDinhMucService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DichVuDienDinhMucService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> RemoveDienDinhMuc(int MaDM)
        {
            var dinhMuc = await _unitOfWork.DienDinhMucs.CheckByID(MaDM);
            if(dinhMuc != null)
            {
                await _unitOfWork.DienDinhMucs.DeleteAsync(dinhMuc);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateDienDinhMuc(DinhMucDTO dto, string name)
        {
            var dienDinhMuc = await _unitOfWork.DienDinhMucs.CheckByID(dto.MaDM);
            if(dienDinhMuc != null)
            {
                dienDinhMuc.TenDM = dto.TenDM;
                dienDinhMuc.ChiSoDau = dto.ChiSoDau;
                dienDinhMuc.ChiSoCuoi = dto.ChiSoCuoi;
                dienDinhMuc.DonGiaDinhMuc = dto.DonGia;
                dienDinhMuc.Description = dto.Description;
                dienDinhMuc.NguoiSua = name;
                await _unitOfWork.DienDinhMucs.UpdateAsync(dienDinhMuc);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CreateDinhMuc> CreateNewDinhMuc(CreateDinhMuc dto, string name)
        {
            var dinhMucCheck = await _unitOfWork.DienDinhMucs.CheckDinhMuc(dto);
            if (dinhMucCheck == null)
            {
                var dienDinhMuc = _mapper.Map<dvDienDinhMuc>(dto);
                dienDinhMuc.NguoiTao = name;
                await _unitOfWork.DienDinhMucs.AddAsync(dienDinhMuc);
                await _unitOfWork.SaveChangesAsync();
                return dto;
            }
            return null;
        }

        public async Task<List<DinhMucDTO>> GetDSDinhMucDien()
        {
            var dsDinhMuc = await _unitOfWork.DienDinhMucs.GetDSDienDinhMuc();
            return dsDinhMuc;
        }
    }
}
