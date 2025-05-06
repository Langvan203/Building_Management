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
    public class MatBangService : IMatBangService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MatBangService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MatBangDto> BanGiaoMatBang(int MaMB, int MaKH)
        {
            var checkMB = await _unitOfWork.MatBangs.GetFirstOrDefaultAsync(x => x.MaMB == MaMB);
            if (checkMB != null)
            {
                checkMB.MaKH = MaKH;
                checkMB.IsBanGiao = true;
                await _unitOfWork.MatBangs.UpdateAsync(checkMB);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<MatBangDto>(checkMB);
            }
            return null;
        }

        public async Task<MatBangDto> CreateMatBang(CreateMatBangDto dto, string name)
        {
            var checkTTMB = await _unitOfWork.MatBangs.GetFirstOrDefaultAsync(x => x.MaVT == dto.MaVT);
            if (checkTTMB == null)
            {
                var newMB = _mapper.Map<tnMatBang>(dto);
                newMB.NguoiTao = name;
                await _unitOfWork.MatBangs.AddAsync(newMB);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<MatBangDto>(newMB);
            }
            return null;
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaKH(int MaKH)
        {
            var dsMB = await _unitOfWork.MatBangs.GetDSMatBangByMaKH(MaKH);
            return dsMB;
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaLMB(int MaLMB, int MaTN)
        {
            var dsMB = await _unitOfWork.MatBangs.GetDSMatBangByMaLMB(MaLMB,MaTN);
            return dsMB;
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTL(int MaTL, int MaTN)
        {
            var dsMB = await _unitOfWork.MatBangs.GetDSMatBangByMaTL(MaTL, MaTN);
            return dsMB;
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTN(int MaTN)
        {
            var dsMB = await _unitOfWork.MatBangs.GetDSMatBangByMaTN(MaTN);
            return dsMB;
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTT(int MaTT, int MaTN)
        {
            var dsMB = await _unitOfWork.MatBangs.GetDSMatBangByMaTT(MaTT, MaTN);
            return dsMB;
        }

        public async Task<bool> RemoveMatBang(int MaMB)
        {
            var checkMB = await _unitOfWork.MatBangs.GetFirstOrDefaultAsync(x => x.MaMB == MaMB);
            if(checkMB != null)
            {
                await _unitOfWork.MatBangs.DeleteAsync(checkMB);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<MatBangDto> UpdateMatBang(int MaMB, UpdateThongTinCoBanMatBangDto dto, string name)
        {
            var checkMB = await _unitOfWork.MatBangs.GetFirstOrDefaultAsync(x => x.MaMB == MaMB);
            if(checkMB != null)
            {
                checkMB.DienTichThongThuy = dto.DienTichThongThuy;
                checkMB.DienTichTimTuong = dto.DienTichTimTuong;
                checkMB.NgayBanGiao = dto.NgayBanGiao;
                checkMB.NgayHetHanChoThue = dto.NgayHetHanChoThue;
                checkMB.DienTichBG = dto.DienTichBG;
                checkMB.NguoiSua = name;
                await _unitOfWork.MatBangs.UpdateAsync(checkMB);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<MatBangDto>(checkMB);
            }
            return null;
        }
    }
}
