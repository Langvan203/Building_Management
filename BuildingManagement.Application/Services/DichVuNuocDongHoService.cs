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
using System.Xml.Linq;

namespace BuildingManagement.Application.Services
{
    public class DichVuNuocDongHoService : IDichVuNuocDongHoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuNuocDongHoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateDongHoDto> CreateNuocDongHo(CreateDongHoDto dto, string name)
        {
            var checkDongHo = await _unitOfWork.NuocDongHos.CheckThemDongHoNuoc(dto);
            if (checkDongHo)
            {
                var nuocDongHo = _mapper.Map<dvNuocDongHo>(dto);
                nuocDongHo.NguoiTao = name;
                await _unitOfWork.NuocDongHos.AddAsync(nuocDongHo);
                await _unitOfWork.SaveChangesAsync();
                return dto;
            }
            return null;
        }

        public async Task<PagedResult<DongHoDTO>> GetDSNuocDongHo(int pageNumber)
        {
            var dsDongHo = await _unitOfWork.NuocDongHos.GetDSDongHoNuocPaged(pageNumber);
            return dsDongHo;
        }

        public async Task<bool> GhiChiSoMoi(int MaDH, int ChiSoMoi, string name)
        {
            var checkDongHo = await _unitOfWork.NuocDongHos.CheckDongHo(MaDH);
            if (checkDongHo != null && ChiSoMoi > checkDongHo.ChiSoSuDung)
            {
                checkDongHo.ChiSoSuDung = ChiSoMoi;
                checkDongHo.NguoiSua = name;
                await _unitOfWork.NuocDongHos.UpdateAsync(checkDongHo);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveNuocDongHo(int MaDH)
        {
            var checkDongHo = await _unitOfWork.NuocDongHos.CheckDongHo(MaDH);
            if (checkDongHo != null)
            {
                if(checkDongHo.TrangThai == true)
                {
                    return false;
                }    
                await _unitOfWork.NuocDongHos.DeleteAsync(checkDongHo);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateNuocDongHo(UpdateDongHoDto dto, string name)
        {
            var checkDongHo = await _unitOfWork.NuocDongHos.CheckDongHo(dto.MaDH);
            if (checkDongHo != null)
            {
                checkDongHo.ChiSoSuDung = dto.ChiSoSuDung;
                checkDongHo.TrangThai = dto.TrangThai;
                checkDongHo.NguoiSua = name;
                checkDongHo.UpdatedDate = DateTime.Now;
                await _unitOfWork.NuocDongHos.UpdateAsync(checkDongHo);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
         
        public async Task<bool> UpdateTrangThai(int MaDH, bool TrangThai, string Name)
        {
            var checkDongHo = await _unitOfWork.NuocDongHos.CheckDongHo(MaDH);
            if (checkDongHo != null)
            {
                checkDongHo.TrangThai = TrangThai;
                checkDongHo.NguoiSua = Name;
                checkDongHo.UpdatedDate = DateTime.Now;
                await _unitOfWork.NuocDongHos.UpdateAsync(checkDongHo);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
