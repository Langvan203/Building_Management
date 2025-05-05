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
    public class TrangThaiMatBangService : ITrangThaiMatBangService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrangThaiMatBangService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TrangThaiMatBangDto> CreateNewTrangThaiMB(CreateNewTrangThaiMatBangDto dto, string HoTen)
        {
            var checkTrangThai = await _unitOfWork.TrangThaiMatBangs.GetFirstOrDefaultAsync(x => x.TenTrangThai == dto.TenTrangThai);
            if (checkTrangThai == null)
            {
                var newTTMatBang = _mapper.Map<mbTrangThai>(dto);
                await _unitOfWork.TrangThaiMatBangs.AddAsync(newTTMatBang);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<TrangThaiMatBangDto>(newTTMatBang);
            }
            throw new Exception("Trạng thái đã tồn tại");
        }

        public async Task<IEnumerable<TrangThaiMatBangDto>> GetDSTrangThaiMatBang()
        {
            var dsTrangThai = await _unitOfWork.TrangThaiMatBangs.GetDSTrangThai();
            if (dsTrangThai == null)
                return null;
            return dsTrangThai;
        }

        public async Task<bool> RemoveTrangThai(int TrangThaiMB)
        {
            var checkTT = await _unitOfWork.TrangThaiMatBangs.GetFirstOrDefaultAsync(x => x.MaTrangThai == TrangThaiMB);
            if (checkTT == null)
                return false;
            await _unitOfWork.TrangThaiMatBangs.DeleteAsync(checkTT);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
