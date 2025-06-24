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
    public class DichVuNuocDinhMucService : IDichVuNuocDinhMucService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuNuocDinhMucService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateDinhMuc> CreateNewDinhMuc(CreateDinhMuc dto, string name)
        {
            var dinhMucCheck = await _unitOfWork.NuocDinhMucs.CheckDinhMuc(dto);
            if (dinhMucCheck == null)
            {
                var nuocDinhMuc = _mapper.Map<dvNuocDinhMuc>(dto);
                nuocDinhMuc.NguoiTao = name;
                await _unitOfWork.NuocDinhMucs.AddAsync(nuocDinhMuc);
                await _unitOfWork.SaveChangesAsync();
                return dto;
            }
            return null;
        }

        public async Task<List<DinhMucDTO>> GetDSDinhMucNuoc()
        {
            var dsDinhMuc = await _unitOfWork.NuocDinhMucs.GetDSNuocDinhMuc();
            return dsDinhMuc;
        }

        public async Task<bool> RemoveNuocDinhMuc(int MaDM)
        {
            var dinhMuc = await _unitOfWork.NuocDinhMucs.CheckByID(MaDM);
            if (dinhMuc != null)
            {
                await _unitOfWork.NuocDinhMucs.DeleteAsync(dinhMuc);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateNuocDinhMuc(DinhMucDTO dto, string name)
        {
            var nuocDinhMuc = await _unitOfWork.NuocDinhMucs.CheckByID(dto.MaDM);
            if (nuocDinhMuc != null)
            {
                nuocDinhMuc.TenDM = dto.TenDM;
                nuocDinhMuc.ChiSoDau = dto.ChiSoDau;
                nuocDinhMuc.ChiSoCuoi = dto.ChiSoCuoi;
                nuocDinhMuc.DonGiaDinhMuc = dto.DonGia;
                nuocDinhMuc.Description = dto.Description;
                nuocDinhMuc.NguoiSua = name;
                await _unitOfWork.NuocDinhMucs.UpdateAsync(nuocDinhMuc);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
