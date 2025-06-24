using AutoMapper;
using BuildingManagement.Application.DTOs;
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
    public class NKBTChiTietBaoTriService : INKBTChiTietBaoTriService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public NKBTChiTietBaoTriService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateChiTietBaoTri(int MaKeHoach, List<CreateChiTietBaoTriDto> dsCongViec)
        {
            var newChiTietBaoTri = _mapper.Map<List<nkbtChiTietBaoTri>>(dsCongViec);
            newChiTietBaoTri.ForEach(x => x.MaKeHoach = MaKeHoach);

            await _unitOfWork.ChiTietBaoTris.AddRangeAsync(newChiTietBaoTri);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
