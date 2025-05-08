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
    public class DichVuSuDungRepository : Repository<dvDichVuSuDung>, IDichVuSuDungRepository
    {
        private readonly IMapper _mapper;
        public DichVuSuDungRepository(BuildingManagementDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<DichVuSuDungDto>> GetDSDichVuSuDungByMaKH(int MaKH)
        {
            var dsDVSuDung = await _context.dvDichVuSuDungs.Where(x => x.MaKH == MaKH).ToListAsync();
            return _mapper.Map<IEnumerable<DichVuSuDungDto>>(dsDVSuDung);
        }
    }
    
}
