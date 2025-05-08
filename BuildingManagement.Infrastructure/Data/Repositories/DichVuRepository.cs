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
    public class DichVuRepository : Repository<dvDichVu>, IDichVuRepository
    {
        private readonly IMapper _mapper;
        public DichVuRepository(BuildingManagementDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<DichVuDto>> GetDVByMaLDV(int MaLDV)
        {
            var dsDichVu = await _context.dvDichVus.Where(x => x.MaLDV == MaLDV).ToListAsync();
            return _mapper.Map<IEnumerable<DichVuDto>>(dsDichVu);
        }
    }
    
}
