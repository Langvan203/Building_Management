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
    public class LoaiMatBangRepository : Repository<mbLoaiMB>, IMatBangLoaiMatBangRepository
    {
        private readonly IMapper _mapper;
        public LoaiMatBangRepository(BuildingManagementDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }


        public async Task<IEnumerable<LoaiMatBangDto>> GetDSLoaiMB()
        {
            var dsLMB = await _context.mbLoaiMBs.ToListAsync();
            if(dsLMB != null)
                return _mapper.Map<IEnumerable<LoaiMatBangDto>>(dsLMB);
            return null;
        }


    }
}
