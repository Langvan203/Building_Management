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
    public class TrangThaiMatBangRepository : Repository<mbTrangThai>, IMatBangTrangThaiRepository
    {
        private readonly IMapper _mapper;
        public TrangThaiMatBangRepository(BuildingManagementDbContext context, IMapper mapper) : base(context) 
        {
            _mapper = mapper;
        }
        public async Task<IEnumerable<TrangThaiMatBangDto>> GetDSTrangThai()
        {
            var dsTrangThai = await _context.mbTrangThais.ToListAsync();
            if(dsTrangThai!= null)
            {
                return _mapper.Map<IEnumerable<TrangThaiMatBangDto>>(dsTrangThai);
            }
            return null;
        }
    }
}
