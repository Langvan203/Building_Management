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
    public class KhoiNhaService : IKhoiNhaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public KhoiNhaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<KhoiNhaDto> CreateKhoiNha(CreateKhoiNhaDto request, string TenNV)
        {
            var checkKhoiNha = await _unitOfWork.KhoiNhas.ExistsAsync(x => x.MaTN == request.MaTN && x.TenKN == request.TenKN);
            if (checkKhoiNha)
            {
                throw new Exception($"Khối nhà trong mã tòa nhà {request.MaTN} đã tồn tại");
            }
            var newKN = _mapper.Map<tnKhoiNha>(request);
            newKN.MaTN = request.MaTN;
            newKN.NguoiTao = TenNV;
            await _unitOfWork.KhoiNhas.AddAsync(newKN);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<KhoiNhaDto>(newKN);
        }

        public async Task<IEnumerable<KhoiNhaDto>> GetKhoiNhaByMaTN(int matn)
        {
            var dskn = await _unitOfWork.KhoiNhas.GetDSKhoiNhaByMaTN(matn);
            return dskn;
        }
    }
}
