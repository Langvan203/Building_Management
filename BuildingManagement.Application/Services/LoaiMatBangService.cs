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
    public class LoaiMatBangService : ILoaiMatBangService
    {
        private readonly IUnitOfWork _uitOfWork;
        private readonly IMapper _mapper;
        public LoaiMatBangService(IUnitOfWork uitOfWork, IMapper mapper)
        {
            _uitOfWork = uitOfWork;
            _mapper = mapper;
        }
        public async Task<LoaiMatBangDto> CreateNewLoaiMB(CreateNewLoaiMB dto, string Name)
        {
            var checkMB = await _uitOfWork.LoaiMatBangs.ExistsAsync(x => x.TenLMB == dto.TenLMB);
            if(!checkMB)
            {
                var newLMB = _mapper.Map<mbLoaiMB>(dto);
                newLMB.NguoiTao = Name;
                await _uitOfWork.LoaiMatBangs.AddAsync(newLMB);
                await _uitOfWork.SaveChangesAsync();
                return _mapper.Map<LoaiMatBangDto>(newLMB);
            }
            return null;
        }

        public async Task<bool> DeleteLoaiMB(int MaLMB)
        {
            var checkLMB = await _uitOfWork.LoaiMatBangs.GetFirstOrDefaultAsync(x => x.MaLMB == MaLMB);
            if(checkLMB != null)
            {
                await _uitOfWork.LoaiMatBangs.DeleteAsync(checkLMB);
                await _uitOfWork.SaveChangesAsync();
                return true;
            }   
            return false;
        }

        public async Task<IEnumerable<LoaiMatBangDto>> GetDSLoaiMB()
        {
            var dsMB = await _uitOfWork.LoaiMatBangs.GetDSLoaiMB();
            if (dsMB == null)
                return null;
            return dsMB;
        }
    }
}
