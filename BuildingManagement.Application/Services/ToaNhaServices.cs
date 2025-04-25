using AutoMapper;
using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
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

        public async Task<IEnumerable<ToaNhaDto>> GetToaNhaAsync()
        {
            var dsToanha = await _unitOfWork.ToaNhas.GetAllAsync();
            return _mapper.Map<IEnumerable<ToaNhaDto>>(dsToanha);
        }

        public async Task<ToaNhaDto> GetToaNhaTheoIdAsync(int id)
        {
            var toanha = await _unitOfWork.ToaNhas.GetByIdAsync(id);
            return _mapper.Map<ToaNhaDto>(toanha);
        }

        public async Task TaoToaNhaAsync(CreateToaNhaDto dto)
        {
            
        }
    }
}
