using AutoMapper;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class DichVuGuiXeTheXeRepository : Repository<dvgxTheXe>, IDichVuGuiXeTheXeRepository
    {
        private readonly IMapper _mapper;
        public DichVuGuiXeTheXeRepository(BuildingManagementDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<DichVuGuiXeTheXeDto>> GetDSTheXeByMaKH(int MaKH)
        {
            var dsTheXe = await _context.dvgxTheXes.Where(x => x.MaKH == MaKH).ToListAsync();
            return _mapper.Map<IEnumerable<DichVuGuiXeTheXeDto>>(dsTheXe);
        }

        public async Task<IEnumerable<DichVuGuiXeTheXeDto>> GetTheXeByMaLX(int MaLX)
        {
            var dsTheXe = await _context.dvgxTheXes.Where(x => x.MaLX == MaLX).ToListAsync();
            return _mapper.Map<IEnumerable<DichVuGuiXeTheXeDto>>(dsTheXe);
        }

        public async Task<IEnumerable<DichVuGuiXeTheXeDto>> GetTheXeByMaMB(int MaMB)
        {
            var dsTheXe = await _context.dvgxTheXes.Where(x => x.MaMB == MaMB).ToListAsync();
            return _mapper.Map<IEnumerable<DichVuGuiXeTheXeDto>>(dsTheXe);
        }
    }
    
}
