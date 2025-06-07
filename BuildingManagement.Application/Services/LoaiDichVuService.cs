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

        public async Task<CreateLoaiDVDto> CreateLoaiDichVu(CreateLoaiDVDto dto, string tennv)
        {
            var LoaiDichVu = await _unitOfWork.LoaiDichVus.GetFirstOrDefaultAsync(x => x.TenLDV == dto.TenLDV && x.MaTN == dto.MaTN);
            if (LoaiDichVu != null)
            {
                throw new Exception("Loại dịch vụ đã tồn tại.");
            }
            var newLoaiDichVu = _mapper.Map<dvLoaiDV>(dto);
            newLoaiDichVu.NguoiTao = tennv;
            await _unitOfWork.LoaiDichVus.AddAsync(newLoaiDichVu);
            await _unitOfWork.SaveChangesAsync();
            return dto;
        }


        public async Task<bool> DeleteLoaiDichVu(int MaLDV)
        {
            var LoaiDichVu = await _unitOfWork.LoaiDichVus.GetFirstOrDefaultAsync(x => x.MaLDV == MaLDV);
            if (LoaiDichVu == null)
            {
                throw new Exception("Loại dịch vụ không tồn tại.");
            }
            await _unitOfWork.LoaiDichVus.DeleteAsync(LoaiDichVu);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<List<GetDSLoaiDichVu>> GetDSLoaiDichVu()
        {
            var dsLoaiDichVu = await _unitOfWork.LoaiDichVus.GetDSLoaiDichVu();
            return dsLoaiDichVu;
        }

        public async Task<List<GetDSLoaiDichVu>> GetDSLoaiDichVuByMaTN(int MaTN)
        {
            var dsLoaiDichVu = await _unitOfWork.LoaiDichVus.GetDSLoaiDichVuByMaTN(MaTN);
            return dsLoaiDichVu;
        }

        
    }
}
