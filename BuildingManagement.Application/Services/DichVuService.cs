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
    public class DichVuService : IDichVuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DichVuDto> CreateNewDichVu(CreateDichVuDto dto, string name)
        {
            var checkDV = await _unitOfWork.DichVus.GetFirstOrDefaultAsync(x => x.MaLDV == dto.MaLDV && x.TenDV == dto.TenDV);
            if(checkDV != null)
            {
                return null;
            }
            var newDV = _mapper.Map<dvDichVu>(dto);
            newDV.NguoiTao = name;
            await _unitOfWork.DichVus.AddAsync(newDV);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DichVuDto>(newDV);
        }

        public async Task<IEnumerable<DichVuDto>> GetDSDichVu()
        {
            var dsDichVu = await _unitOfWork.DichVus.GetAllAsync();
            return _mapper.Map<IEnumerable<DichVuDto>>(dsDichVu);
        }

        public async Task<IEnumerable<DichVuDto>> GetDVByMaLDV(int MaLDV)
        {
            var dsDichVu = await _unitOfWork.DichVus.GetDVByMaLDV(MaLDV);
            return dsDichVu;
        }
    }
}
