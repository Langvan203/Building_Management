using AutoMapper;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<List<KhoiNhaDto>> GetDSKhoiNhaDetail()
        {
            var dskn = await _unitOfWork.KhoiNhas.GetDSKhoiNhaDetail();
            return dskn;
        }

        public async Task<bool> DeleteKhoiNha(int MaKN)
        {
            var khoinha = await _unitOfWork.KhoiNhas.GetByIdAsync(MaKN);
            if (khoinha == null)
            {
                throw new Exception($"Khối nhà có mã {MaKN} không tồn tại");
            }
            khoinha.TrangThaiKhoiNha = 0;
            _unitOfWork.KhoiNhas.DeleteAsync(khoinha);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateKhoiNha(UpdateKhoiNhaDto dto, string tennv)
        {
            var findKhoiNha = await _unitOfWork.KhoiNhas.GetFirstOrDefaultAsync(x => x.MaKN == dto.MaKN);
            if(findKhoiNha == null)
            {
                return false;
            }
            findKhoiNha.TenKN = dto.TenKN;
            findKhoiNha.TrangThaiKhoiNha = dto.TrangThaiKhoiNha;
            findKhoiNha.NguoiSua = tennv;
            findKhoiNha.UpdatedDate = DateTime.Now;
            await _unitOfWork.KhoiNhas.UpdateAsync(findKhoiNha);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<KhoiNhaFilter>> GetKhoiNhaFilter()
        {
            var dskn = await _unitOfWork.KhoiNhas.GetKhoiNhaFilter();
            if (dskn == null)
            {
                throw new Exception("Không có khối nhà nào");
            }
            return dskn;
        }
    }
}
