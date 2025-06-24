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
    public class DichVuGuiXeLoaiXeService : IDichVuGuiXeLoaiXeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuGuiXeLoaiXeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DichVuGuiXeLoaiXeDto> CreateNewLoaiXe(CreateDichVuGuiXeLoaiXeDto dto, string name)
        {
            var checkLoaiXe = await _unitOfWork.LoaiXes.GetFirstOrDefaultAsync(x => x.TenLX == dto.TenLX);
            if (checkLoaiXe == null)
            {
                var newLX = _mapper.Map<dvgxLoaiXe>(dto);
                newLX.NguoiTao = name;
                await _unitOfWork.LoaiXes.AddAsync(newLX);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<DichVuGuiXeLoaiXeDto>(newLX);
            }
            return null;
        }

        public async Task<IEnumerable<DichVuGuiXeLoaiXeDto>> GetDSLoaiXe()
        {
            var dsLoaiXe = await _unitOfWork.LoaiXes.GetAllAsync();
            return _mapper.Map<IEnumerable<DichVuGuiXeLoaiXeDto>>(dsLoaiXe);
        }
    }
}
