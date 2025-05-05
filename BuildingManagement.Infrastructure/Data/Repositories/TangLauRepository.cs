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
    public class TangLauRepository : Repository<tnTangLau>, ITangLauRepository
    {
        private readonly IMapper _mapper;
        public TangLauRepository(BuildingManagementDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<TangLauDto>> GetDSTangLau(int MaTN, int MaKN)
        {
            var dsTL = await _context.tnTangLaus.Where(x => x.MaKN == MaKN && x.tnKhoiNha.MaKN == MaKN).ToListAsync();
            return _mapper.Map<IEnumerable<TangLauDto>>(dsTL);
        }

        public async Task<bool> CheckTangLau(int MaKN, int MaTN)
        {
            var checkKN = await _context.tnKhoiNhas.Where(x => x.MaKN == MaKN && x.MaTN == MaTN).FirstOrDefaultAsync();
            if (checkKN != null)
            {
                return true;
            }
            return false;
        }
    }
}
