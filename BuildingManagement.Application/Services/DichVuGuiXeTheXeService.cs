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
    public class DichVuGuiXeTheXeService : IDichVuGuiXeTheXeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuGuiXeTheXeService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DichVuGuiXeTheXeDto> CreateNewTheXe(CreateDichVuGuiXeTheXeDto dto, string name)
        {
            var newTX = _mapper.Map<dvgxTheXe>(dto);
            await _unitOfWork.TheXes.AddAsync(newTX);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DichVuGuiXeTheXeDto>(newTX);
        }

        public async Task<IEnumerable<DichVuGuiXeTheXeDto>> GetDSTheXe()
        {
            var dsTheXe = await _unitOfWork.TheXes.GetAllAsync();
            return _mapper.Map<IEnumerable<DichVuGuiXeTheXeDto>>(dsTheXe);
        }

        public async Task<IEnumerable<DichVuGuiXeTheXeDto>> GetDSTheXeByMaKH(int MaKH)
        {
            var dsTheXe = await _unitOfWork.TheXes.GetDSTheXeByMaKH(MaKH);
            return dsTheXe;
        }

        public async Task<IEnumerable<DichVuGuiXeTheXeDto>> GetTheXeByMaLX(int MaLX)
        {
            var dsTheXe = await _unitOfWork.TheXes.GetTheXeByMaLX(MaLX);
            return dsTheXe;
        }

        public async Task<IEnumerable<DichVuGuiXeTheXeDto>> GetTheXeByMaMB(int MaMB)
        {
            var dsTheXe = await _unitOfWork.TheXes.GetTheXeByMaMB(MaMB);
            return dsTheXe;
        }
    }
}
