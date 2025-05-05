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
    public class ToaNhaServices : IToaNhaServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ToaNhaServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> XoaToaNhaAsync(int id)
        {
            var findTN = await _unitOfWork.ToaNhas.GetByIdAsync(id);
            if (findTN == null)
            {
                return false;
            }
            await _unitOfWork.ToaNhas.DeleteAsync(findTN);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ToaNhaDto>> GetDSToaNhaAsync()
        {
            var dsToanha = await _unitOfWork.ToaNhas.GetAllAsync();
            return _mapper.Map<IEnumerable<ToaNhaDto>>(dsToanha);
        }

        public async Task<ToaNhaDto> GetToaNhaTheoIdAsync(int id)
        {
            var toanha = await _unitOfWork.ToaNhas.GetByIdAsync(id);
            return _mapper.Map<ToaNhaDto>(toanha);
        }

        public async Task<tnToaNha> TaoToaNhaAsync(CreateToaNhaDto dto, string tennv)
        {
            var findToaNha = await _unitOfWork.ToaNhas.ExistsAsync(x => x.TenTN == dto.TenTN);
            if(findToaNha)
            {
                throw new Exception("Dự án tòa nhà đã tồn tại");
            }    
            var newToaNha = _mapper.Map<tnToaNha>(dto);
            newToaNha.NguoiTao = tennv;
            await _unitOfWork.ToaNhas.AddAsync(newToaNha);
            await _unitOfWork.SaveChangesAsync();
            return newToaNha;
        }
    }
}
