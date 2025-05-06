using AutoMapper;
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
    public class LoaiDichVuService : ILoaiDichVuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LoaiDichVuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<LoaiDVDto> CreateNewLoaiDV(CreateLoaiDVDto loaiDV, string name)
        {
            var checkLDV = await _unitOfWork.LoaiDichVus.GetFirstOrDefaultAsync(x => x.TenLDV == loaiDV.TenLDV);
            if(checkLDV != null)
            {
                return null;
            }    
            var newLDV = _mapper.Map<dvLoaiDV>(loaiDV);
            newLDV.NguoiTao = name;
            await _unitOfWork.LoaiDichVus.AddAsync(newLDV);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<LoaiDVDto>(newLDV);
        }

        public async Task<bool> DeleteLoaiDV(int MaLDV)
        {
            var checkLDV = await _unitOfWork.LoaiDichVus.GetFirstOrDefaultAsync(x => x.MaLDV == MaLDV);
            if (checkLDV != null)
            {
                await _unitOfWork.LoaiDichVus.DeleteAsync(checkLDV);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<LoaiDVDto>> GetDSLoaiDV()
        {
            var dsLoaiDV = await _unitOfWork.LoaiDichVus.GetAllAsync();
            return _mapper.Map<IEnumerable<LoaiDVDto>>(dsLoaiDV);
        }
    }
}
