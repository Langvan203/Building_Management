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
    public class KhachHangService : IKhacHangService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public KhachHangService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<KhachHangDto> CreateNewKhachHang(CreateKhachHangDto dto, string name)
        {
            var checkKhachHang = await _unitOfWork.KhachHangs.GetFirstOrDefaultAsync(x => x.CCCD == dto.CCCD && x.HoTen == dto.HoTen);
            if(checkKhachHang != null)
            {
                return null;
            }
            var newKhachHang = _mapper.Map<tnKhachHang>(dto);
            newKhachHang.NguoiTao = name;
            await _unitOfWork.KhachHangs.AddAsync(newKhachHang);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<KhachHangDto>(newKhachHang);
        }

        public async Task<IEnumerable<KhachHangDto>> GetDSKhachHang() => _mapper.Map<IEnumerable<KhachHangDto>>(await _unitOfWork.KhachHangs.GetAllAsync());

        public async Task<List<KhachHangFilter>> GetDSKhachHangFilter()
        {
            var dsKH = await _unitOfWork.KhachHangs.GetDSKhachHang();
            return dsKH;
        }
    }
}
