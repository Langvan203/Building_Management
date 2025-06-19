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
using System.Xml;

namespace BuildingManagement.Application.Services
{
    public class DichVuSuDungService : IDichVuSuDungSerivce
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuSuDungService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DichVuSuDungDto> CreateDichVuSuDung(CreateDichVuSuDungDto dto, string name)
        {
            var checkDVSuDung = await _unitOfWork.DichVuSuDungs.GetFirstOrDefaultAsync(x => x.MaDV == dto.MaDV && x.MaKH == dto.MaKH 
                                        && x.NgayBatDauTinhPhi.Month == dto.NgayBatDauTinhPhi.Month && x.NgayBatDauTinhPhi.Year == dto.NgayBatDauTinhPhi.Year);
            if(checkDVSuDung != null)
            {
                return null;
            }
            var newDVSuDung = _mapper.Map<dvDichVuSuDung>(dto);
            newDVSuDung.NguoiTao = name;
            await _unitOfWork.DichVuSuDungs.AddAsync(newDVSuDung);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DichVuSuDungDto>(newDVSuDung);
        }

        public async Task<IEnumerable<DichVuSuDungDto>> GetDSDichVuSuDungByMaKH(int MaKH)
        {
            var dsDVSuDung = await _unitOfWork.DichVuSuDungs.GetDSDichVuSuDungByMaKH(MaKH);
            return dsDVSuDung;
        }



        public async Task<IEnumerable<DichVuSuDungDto>> GetDSDichVuSuDung()
        {
            var dsDVSuDung = await _unitOfWork.DichVuSuDungs.GetAllAsync();
            return _mapper.Map<IEnumerable<DichVuSuDungDto>>(dsDVSuDung);
        }

        public Task<List<GetDSDichVuSuDung>> GetDSDichVuSuDungByCuDan(int MaKH)
        {
            var dsDVSuDung = _unitOfWork.DichVuSuDungs.GetDSDichVuSuDungByCuDan(MaKH);
            if (dsDVSuDung == null)
            {
                return Task.FromResult(new List<GetDSDichVuSuDung>());
            }
            return dsDVSuDung;
        }

        public Task<List<GetAllDSDichVuSuDung>> GetAllDSDichVuSuDungs()
        {
            var dsDVSuDung = _unitOfWork.DichVuSuDungs.GetAllDSDichVuSuDungs();
            if (dsDVSuDung == null)
            {
                return Task.FromResult(new List<GetAllDSDichVuSuDung>());
            }
            return dsDVSuDung;
        }
    }
}
