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
    public class DichVuDienDongHoSerivce : IDichVuDienDongHoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuDienDongHoSerivce(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateDongHoDto> CreateDienDongHo(CreateDongHoDto dto, string name)
        {
            var checkDongHo = await _unitOfWork.DienDongHos.CheckThemDongHoDien(dto);
            if(checkDongHo)
            {
                var dienDongHo = _mapper.Map<dvDienDongHo>(dto);
                dienDongHo.NguoiTao = name;
                await _unitOfWork.DienDongHos.AddAsync(dienDongHo);
                await _unitOfWork.SaveChangesAsync();
                return dto;
            }
            return null;
        }

        public async Task<PagedResult<DongHoDTO>> GetDSDienDongHo(int pageNumber)
        {
            var dsDongHo = await _unitOfWork.DienDongHos.GetDSDongHoDienPaged(pageNumber);
            return dsDongHo;
        }

        public async Task<bool> RemoveDienDongHo(int MaDH)
        {
            var checkDongHo = await _unitOfWork.DienDongHos.CheckDongHo(MaDH);
            if (checkDongHo != null)
            {
                await _unitOfWork.DienDongHos.DeleteAsync(checkDongHo);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateDienDongHo(UpdateDongHoDto dto, string name)
        {
            var checkDongHo = await _unitOfWork.DienDongHos.CheckDongHo(dto.MaDH);
            if (checkDongHo != null)
            {
                checkDongHo.ChiSoSuDung = dto.ChiSoSuDung;
                checkDongHo.TrangThai = dto.TrangThai;
                checkDongHo.NguoiSua = name;
                checkDongHo.UpdatedDate = DateTime.Now;
                await _unitOfWork.DienDongHos.UpdateAsync(checkDongHo);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
