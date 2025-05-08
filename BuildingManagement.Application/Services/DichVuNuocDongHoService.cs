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
    public class DichVuNuocDongHoService : IDichVuNuocDongHoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuNuocDongHoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DichVuNuocDongHoDto> CreateNewDongHo(CreateDichVuNuocDongHoDto dto, string name)
        {
            var checkDongHo = _unitOfWork.DienDongHos.GetFirstOrDefaultAsync(x => x.SoDongHo == dto.SoDH);
            if(checkDongHo != null)
            {
                return null;
            }
            var newDongHo = _mapper.Map<dvNuocDongHo>(dto);
            newDongHo.NguoiTao = name;
            await _unitOfWork.NuocDongHos.AddAsync(newDongHo);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DichVuNuocDongHoDto>(newDongHo);

        }

        public async Task<DichVuNuocDongHoDto> GetDongHoNuocByMaMB(int MaMB)
        {
            var dsDongHo = await _unitOfWork.DienDongHos.GetFirstOrDefaultAsync(x => x.MaMB == MaMB);
            if (dsDongHo != null)
            {
                return _mapper.Map<DichVuNuocDongHoDto>(dsDongHo);
            }
            return null;
        }

        public async Task<IEnumerable<DichVuNuocDongHoDto>> GetDSDongHo()
        {
            var dsDongHo = await _unitOfWork.NuocDongHos.GetAllAsync();
            if (dsDongHo != null)
            {
                return _mapper.Map<IEnumerable<DichVuNuocDongHoDto>>(dsDongHo);
            }
            return null;
        }
    }
}
