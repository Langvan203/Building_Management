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
    public class KhoiNhaRepository : Repository<tnKhoiNha>, IKhoiNhaRepository
    {
        private readonly IMapper _mapper;
        public KhoiNhaRepository(BuildingManagementDbContext context, IMapper mapper) : base(context) 
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<KhoiNhaDto>> GetDSKhoiNhaByMaTN(int matn)
        {
            var dsToanha = await _context.tnKhoiNhas.Where(x => x.MaTN == matn).ToListAsync();
            return _mapper.Map<IEnumerable<KhoiNhaDto>>(dsToanha);
        }

       
    }
}
