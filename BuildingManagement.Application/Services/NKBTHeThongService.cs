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
    public class NKBTHeThongService : INKBTHeThongSerivce
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NKBTHeThongService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateHeThong> CreteNewHeThong(CreateHeThong createHeThong, string Name)
        {
            var heThongCheck = await _unitOfWork.HeThongs.GetFirstOrDefaultAsync(x => x.TenHeThong == createHeThong.TenHeThong && x.MaTN == createHeThong.MaTN);
            if (heThongCheck != null)
            {
                return null; // He thong already exists
            }
            var newHeThong = _mapper.Map<tnbtHeThong>(createHeThong);
            await _unitOfWork.HeThongs.AddAsync(newHeThong);
            await _unitOfWork.SaveChangesAsync();
            return createHeThong;
        }

        public async Task<bool> DeleteHeThong(int MaHeThong)
        {
            var heThong = await _unitOfWork.HeThongs.GetFirstOrDefaultAsync(h => h.MaHeThong == MaHeThong);
            if (heThong == null)
            {
                return false;
            }
            await _unitOfWork.HeThongs.DeleteAsync(heThong);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<HeThongDTO>> GetDSHeThong(int pageNumber, int pageSize = 15)
        {
            var dsHeThong = await _unitOfWork.HeThongs.GetDSHeThong(pageNumber, pageSize);
            return dsHeThong;
        }

        public async Task<bool> UpdateHeThong(UpdateHeThongDto updateHeThongDto, string Name)
        {
            var heThong = await _unitOfWork.HeThongs.GetFirstOrDefaultAsync(h => h.MaHeThong == updateHeThongDto.MaHeThong);
            if (heThong == null)
            {
                return false;
            }
            heThong.TenHeThong = updateHeThongDto.TenHeThong;
            heThong.NhanHieu = updateHeThongDto.NhanHieu;
            heThong.Model = updateHeThongDto.Model;
            heThong.TrangThai = updateHeThongDto.TrangThai;
            heThong.SerialNumber = updateHeThongDto.SerialNumber;
            heThong.GhiChu = updateHeThongDto.GhiChu;
            heThong.MaTN = updateHeThongDto.MaTN;
            await _unitOfWork.HeThongs.UpdateAsync(heThong);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
