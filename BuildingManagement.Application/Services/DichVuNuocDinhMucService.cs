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
    public class DichVuNuocDinhMucService : IDichVuNuocDinhMucService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuNuocDinhMucService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DichVuNuocDinhMucDto> CreateNewDinhMuc(CreateDichVuNuocDinhMucDto dto, string name)
        {
            var checkDinhMuc = await _unitOfWork.NuocDinhMucs.GetFirstOrDefaultAsync(x => x.TenDM == dto.TenDM);
            if(checkDinhMuc != null)
            {
                return null;
            }
            var newDinhMuc = _mapper.Map<dvNuocDinhMuc>(dto);
            newDinhMuc.NguoiTao = name;
            await _unitOfWork.NuocDinhMucs.AddAsync(newDinhMuc);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DichVuNuocDinhMucDto>(newDinhMuc);
        }

        public async Task<IEnumerable<DichVuNuocDinhMucDto>> GetDSDinhMucNuoc()
        {
            var dsDinhMuc = await _unitOfWork.NuocDinhMucs.GetAllAsync();
            return _mapper.Map<IEnumerable<DichVuNuocDinhMucDto>>(dsDinhMuc);
        }
    }
}
