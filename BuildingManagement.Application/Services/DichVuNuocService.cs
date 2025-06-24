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
    public class DichVuNuocService : IDichVuNuocService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuNuocService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DichVuNuocDto> CreateNewSDNuoc(CreateDichVuNuocDto dto, string name)
        {
            var checkNewSDNuoc = await _unitOfWork.Nuocs.GetFirstOrDefaultAsync(x => x.NgayBatDauSuDung.Month == dto.NgayBatDauSuDung.Month
                    && x.NgayBatDauSuDung.Year == dto.NgayBatDauSuDung.Year && x.MaDH == dto.MaDH);
            if(checkNewSDNuoc != null)
            {
                return null;
            }
            var newSDNuoc = _mapper.Map<dvNuoc>(dto);
            newSDNuoc.NguoiTao = name;
            await _unitOfWork.Nuocs.AddAsync(newSDNuoc);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DichVuNuocDto>(newSDNuoc);

        }

        public async Task<IEnumerable<DichVuNuocDto>> GetDVNuocByMonthAndYear(int month, int year)
        {
            var dsDVNuoc = await _unitOfWork.Nuocs.GetDVNuocByMonthAndYear(month, year);
            return dsDVNuoc;
        }

        public Task<IEnumerable<DichVuNuocDto>> GetDVNuocByMonthYearAndMaDH(int month, int year, int MaDH)
        {
            var dsDVNuoc = _unitOfWork.Nuocs.GetDVNuocByMonthYearAndMaDH(month, year, MaDH);
            return dsDVNuoc;
        }
    }
}
