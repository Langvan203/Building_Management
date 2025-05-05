using AutoMapper;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services
{
    public class TangLauServices : ITangLauServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TangLauServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TangLauDto> CreateTangLau(CreateTangLauDto dto, int MaKN, string TenNguoiTao, int MaTN)
        {
            var checkTL = await _unitOfWork.TangLaus.CheckTangLau(MaKN, MaTN);
            if (checkTL)
            {
                var newTL = _mapper.Map<tnTangLau>(dto);
                newTL.NguoiTao = TenNguoiTao;
                newTL.MaKN = MaKN;
                await _unitOfWork.TangLaus.AddAsync(newTL);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<TangLauDto>(newTL);
            }
            throw new Exception("Mã khối nhà không tồn tại");
        }

        public async Task<IEnumerable<TangLauDto>> GetDSTangLauByMaKN(int MaTN, int MaKN)
        {
            if (MaKN != null && MaTN != null)
            {
                var dsTL = await _unitOfWork.TangLaus.GetDSTangLau(MaTN, MaKN);
                return dsTL;
            }
            return null;
        }
    }
}
