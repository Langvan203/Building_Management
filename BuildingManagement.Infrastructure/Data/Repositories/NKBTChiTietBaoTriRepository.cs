using AutoMapper;
using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class NKBTChiTietBaoTriRepository : Repository<nkbtChiTietBaoTri>, INKBTChiTietBaoTriRepository
    {
        private readonly IMapper _mapper;
        public NKBTChiTietBaoTriRepository(BuildingManagementDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<bool> CreateChiTietBaoTri(int MaKeHoach, List<CreateChiTietBaoTriDto> dsCongViec)
        {
            var newChiTietBaoTri = _mapper.Map<List<nkbtChiTietBaoTri>>(dsCongViec);
            newChiTietBaoTri.ForEach(x => x.MaKeHoach = MaKeHoach);

            await _context.nkbtChiTietBaoTris.AddRangeAsync(newChiTietBaoTri);
            await _context.SaveChangesAsync();
            return true;
        }
    }
    
}
