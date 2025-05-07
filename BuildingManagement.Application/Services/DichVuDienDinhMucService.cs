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
    public class DichVuDienDinhMucService : IDichVuDienDinhMucService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DichVuDienDinhMucService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DichVuDienDinhMucDto> CreateNewDinhMuc(CreateDichVuDienDinhMucDto dto, string name)
        {
            var checkDinhMuc = await _unitOfWork.DienDinhMucs.GetFirstOrDefaultAsync(x => x.TenDM == dto.TenDM);
            if(checkDinhMuc == null)
            {
                var newDinhMuc = _mapper.Map<dvDienDinhMuc>(dto);
                newDinhMuc.NguoiTao = name;
                await _unitOfWork.DienDinhMucs.AddAsync(newDinhMuc);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<DichVuDienDinhMucDto>(newDinhMuc);
            }
            return null;
        }

        public async Task<IEnumerable<DichVuDienDinhMucDto>> GetDSDinhMucDien()
        {
            var dsDinhMuc = await _unitOfWork.DienDinhMucs.GetAllAsync();
            return _mapper.Map<IEnumerable<DichVuDienDinhMucDto>>(dsDinhMuc);
        }
    }
}
