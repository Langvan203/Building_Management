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

        public async Task<TangLauDto> CreateTangLau(CreateTangLauDto dto, string TenNguoiTao)
        {
            var checkTL = await _unitOfWork.TangLaus.GetFirstOrDefaultAsync(x => x.MaKN == dto.MaKN && x.MaTN == dto.MaTN);
            if (checkTL!= null)
            {
                if(checkTL.TenTL == dto.TenTL)
                {
                    throw new Exception("Tên tầng lầu đã tồn tại");
                }    
                var newTL = _mapper.Map<tnTangLau>(dto);
                newTL.NguoiTao = TenNguoiTao;
                await _unitOfWork.TangLaus.AddAsync(newTL);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<TangLauDto>(newTL);
            }
            throw new Exception("Mã khối nhà,tòa nhà không tồn tại");
        }

        public async Task<bool> DeleteTangLau(int MaTL)
        {
            var findTangLau = await _unitOfWork.TangLaus.GetFirstOrDefaultAsync(x => x.MaTL == MaTL);
            if (findTangLau != null)
            {
                await _unitOfWork.TangLaus.DeleteAsync(findTangLau);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<TangLauDto>> GetDSTangLau()
        {
            var dsTangLau = await _unitOfWork.TangLaus.GetDSTangLau();
            return dsTangLau;
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

        public async Task<List<TangLauDto>> GetDSTangLauByMaKN(int MaKN)
        {
            var dsTL = await _unitOfWork.TangLaus.GetTangLauByKhoiNha(MaKN);
            return dsTL;
        }

        public async Task<List<TangLauFilter>> GetTangLauFilter()
        {
            var dsTL = await _unitOfWork.TangLaus.GetTangLauFilter();
            if (dsTL == null)
            {
                throw new Exception("Không có tầng lầu nào");
            }
            return dsTL;
        }

        public async Task<bool> UpdateTangLau(UpdateTangLauDto tangLauDto, string tennv)
        {
            var findTangLau = await _unitOfWork.TangLaus.GetFirstOrDefaultAsync(x => x.MaTL == tangLauDto.MaTL);
            if(findTangLau!= null)
            {
                findTangLau.TenTL = tangLauDto.TenTL;
                findTangLau.DienTichSan = tangLauDto.DienTichSan;
                findTangLau.DienTichKhuVucDungChung = tangLauDto.DienTichKhuVucDungChung;
                findTangLau.DienTichKyThuaPhuTro = tangLauDto.DienTichKyThuaPhuTro;
                findTangLau.NguoiSua = tennv;
                findTangLau.UpdatedDate = DateTime.Now;
                await _unitOfWork.TangLaus.UpdateAsync(findTangLau);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }    
            return false;
        }
        
    }
}
