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
    public class DichVuService : IDichVuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DichVuDto> CreateNewDichVu(CreateDichVuDto dto, string name)
        {
            var checkDV = await _unitOfWork.DichVus.GetFirstOrDefaultAsync(x => x.MaLDV == dto.MaLDV && x.TenDV == dto.TenDV);
            if (checkDV != null)
            {
                throw new Exception("Dịch vụ đã tồn tại trong loại dịch vụ này.");
            }
            var newDV = _mapper.Map<dvDichVu>(dto);
            newDV.NguoiTao = name;
            await _unitOfWork.DichVus.AddAsync(newDV);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DichVuDto>(newDV);
        }

        public async Task<List<GetDSDichVu>> GetDSDichVu()
        {
            var dsDichVu = await _unitOfWork.DichVus.GetDSDichVu();
            return dsDichVu;
        }

        public async Task<IEnumerable<DichVuDto>> GetDVByMaLDV(int MaLDV)
        {
            var dsDichVu = await _unitOfWork.DichVus.GetDVByMaLDV(MaLDV);
            return dsDichVu;
        }

        public async Task<bool> RemoveDichVu(int MaDV)
        {
            var dichVu = await _unitOfWork.DichVus.GetFirstOrDefaultAsync(x => x.MaDV == MaDV);
            if (dichVu == null)
            {
                throw new Exception("Dịch vụ không tồn tại.");
            }
            await _unitOfWork.DichVus.DeleteAsync(dichVu);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
