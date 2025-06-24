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
    public class DichVuDienService : IDichVuDienService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuDienService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DichVuDienDto> CreateNewSDDien(CreateDichVuDienDto dto, string name)
        {
            var checkDinhMucTonTai = await _unitOfWork.Diens.GetFirstOrDefaultAsync(x => x.MaDH == dto.MaDH && x.MaDM == dto.MaDM && x.NgayBatDauSuDung.Month == dto.NgayBatDauSuDung.Month 
                                                                                && x.NgayBatDauSuDung.Year == dto.NgayBatDauSuDung.Year && x.IsThanhToan == false);
            if(checkDinhMucTonTai != null)
            {
                throw new Exception("Định mức đã tồn tại trong tháng này");
            }
            var newDienSuDung = _mapper.Map<dvDien>(dto);
            newDienSuDung.NguoiTao = name;
            await _unitOfWork.Diens.AddAsync(newDienSuDung);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DichVuDienDto>(newDienSuDung);
        }

        public async Task<IEnumerable<DichVuDienDto>> GetDVDienByMonthAndYear(int month, int year)
        {
            var dsDienTheoThangNam = await _unitOfWork.Diens.GetAllDienByThangNam(month, year);
            return dsDienTheoThangNam;
        }

        public async Task<IEnumerable<DichVuDienDto>> GetDVDienByMonthYearAndMaDH(int month, int year, int MaDH)
        {
            var dsDienTheoThangNamAndMaDH = await _unitOfWork.Diens.GetAllDienByThangNamAndMaDH(month, year, MaDH);
            return dsDienTheoThangNamAndMaDH;
        }
    }
}
