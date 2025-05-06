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
    public class MatBangRepository : Repository<tnMatBang>, IMatBangRepository
    {
        private readonly IMapper _mapper;
        public MatBangRepository(BuildingManagementDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaKH(int MaKH)
        {
            
            var dsMB = await _context.tnMatBangs.Where(x => x.MaKH == MaKH).ToListAsync();
            return _mapper.Map<IEnumerable<MatBangDto>>(dsMB);
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaLMB(int MaLMB, int MaTN)
        {
            var dsMB = await _context.tnMatBangs.Where(x => x.MaLMB == MaLMB && MaTN == MaTN).ToListAsync();
            return _mapper.Map<IEnumerable<MatBangDto>>(dsMB);
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTL(int MaTL, int MaTN)
        {
            var dsMB = await _context.tnMatBangs.Where(x => x.MaTL == MaTL && MaTN == MaTN).ToListAsync();
            return _mapper.Map<IEnumerable<MatBangDto>>(dsMB);
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTN(int MaTN)
        {
            var dsMB = await _context.tnMatBangs.Where(x => x.MaTN == MaTN && MaTN == MaTN).ToListAsync();
            return _mapper.Map<IEnumerable<MatBangDto>>(dsMB);
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTT(int MaTT, int MaTN)
        {
            var dsMB = await _context.tnMatBangs.Where(x => x.MaTrangThai == MaTT && MaTN == MaTN).ToListAsync();
            return _mapper.Map<IEnumerable<MatBangDto>>(dsMB);
        }


    }
}
