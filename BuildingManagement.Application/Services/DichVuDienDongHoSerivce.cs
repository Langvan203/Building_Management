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
    public class DichVuDienDongHoSerivce : IDichVuDienDongHoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuDienDongHoSerivce(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DichVuDienDongHoDto> CreateNewDongHo(CreateDichVuDienDongHoDto dto, string name)
        {
            var checkDongHoDien = await _unitOfWork.DienDongHos.GetFirstOrDefaultAsync(x => x.SoDongHo == dto.SoDongHo);
            if(checkDongHoDien == null)
            {
                var newDh = _mapper.Map<dvDienDongHo>(dto);
                newDh.NguoiTao = name;
                await _unitOfWork.DienDongHos.AddAsync(newDh);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<DichVuDienDongHoDto>(newDh);
            }
            return null;
        }

        public async Task<DichVuDienDongHoDto> GetDongHoDienByMaMB(int MaMB)
        {
            var checkDongHoDien = await _unitOfWork.DienDongHos.GetFirstOrDefaultAsync(x => x.MaMB == MaMB);
            if(checkDongHoDien != null)
            {
                return _mapper.Map<DichVuDienDongHoDto>(checkDongHoDien);
            }
            return null;
        }

        public async Task<IEnumerable<DichVuDienDongHoDto>> GetDSDongHo()
        {
            var dsDongHo = await _unitOfWork.DienDongHos.GetAllAsync();
            return _mapper.Map<IEnumerable<DichVuDienDongHoDto>>(dsDongHo);
        }
    }
}
