using AutoMapper;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Response;
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

        public async Task<List<ToaNhaDto>> GetDSToaNhaAsync()
        {
            var dsToanha = await _unitOfWork.ToaNhas.GetToaNhaDtoAsync();
            return dsToanha;
        }

        public async Task<ToaNhaDto> GetToaNhaTheoIdAsync(int id)
        {
            var toanha = await _unitOfWork.ToaNhas.GetByIdAsync(id);
            return _mapper.Map<ToaNhaDto>(toanha);
        }

        public async Task<ToaNhaDto> TaoToaNhaAsync(CreateToaNhaDto dto, string tennv)
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
            return _mapper.Map<ToaNhaDto>(newToaNha);
        }

        public async Task<SummaryTotalBuildingResponseDtoWithCompare> SummaryTotalBuildingAsync()
        {
            var dataTotalBuilding = await _unitOfWork.ToaNhas.SummaryTotalBuildingAsync();
            return dataTotalBuilding;
        }

        public async Task<IEnumerable<OccupancyRate>> GetOccupancyRateAsync()
        {
            var occupancyRate = await _unitOfWork.ToaNhas.GetOccupancyRateAsync();
            return occupancyRate;
        }

        public async Task<BuildingDataOverView> BuildingsData(DateTime from, DateTime to)
        {
            var buildingData = await _unitOfWork.ToaNhas.BuildingsData(from,to);
            return buildingData;
        }

        public async Task<FinnancesData> GetFinnancesData(DateTime form, DateTime to)
        {
            var finnancesData = await _unitOfWork.ToaNhas.GetFinnancesDataAsync(form, to);
            return finnancesData;
        }

        public Task<ServicesData> GetServicesData(DateTime from, DateTime to)
        {
            var servicesData = _unitOfWork.ToaNhas.GetServicesData(from, to);
            return servicesData;
        }

        public Task<OverViewData> GetOverViewData(int year)
        {
            var overViewData = _unitOfWork.ToaNhas.GetOverViewData(year);
            return overViewData;
        }

        public async Task<ToaNhaDto> UpdateToaNha(UpdateToaNhaDto dto, string tennv)
        {   
            var findToaNhaByName = await _unitOfWork.ToaNhas.GetFirstOrDefaultAsync(x => x.MaTN == dto.Id);
            if (findToaNhaByName == null)
            {
                throw new Exception("Dự án tòa nhà không tồn tại");
            }
            findToaNhaByName.TenTN = dto.TenTN;
            findToaNhaByName.DiaChi = dto.DiaChi;
            findToaNhaByName.TrangThaiToaNha = dto.TrangThaiToaNha;
            findToaNhaByName.SoTangNoi = dto.SoTangNoi;
            findToaNhaByName.SoTangHam = dto.SoTangHam;
            findToaNhaByName.SoTaiKhoan = dto.SoTaiKhoan;
            findToaNhaByName.NganHangThanhToan = dto.NganHangThanhToan;
            findToaNhaByName.NoiDungChuyenKhoan = dto.NoiDungChuyenKhoan;
            findToaNhaByName.DienTichXayDung = dto.DienTichXayDung;
            findToaNhaByName.TongDienTichSan = dto.TongDienTichSan;
            findToaNhaByName.TongDienTichChoThueNET = dto.TongDienTichChoThueNET;
            findToaNhaByName.TongDienTichChoThueGross = dto.TongDienTichChoThueGross;
            findToaNhaByName.NguoiSua = tennv;

            await _unitOfWork.ToaNhas.UpdateAsync(findToaNhaByName);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ToaNhaDto>(findToaNhaByName);
        }
    }
}
