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
    public class DichVuNuocRepository : Repository<dvNuoc>, IDichVuNuocRepository
    {
        private readonly IMapper _mapper;
        public DichVuNuocRepository(BuildingManagementDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<DichVuNuocDto>> GetDVNuocByMonthAndYear(int month, int year)
        {
            var dsDVNuoc = await _context.dvNuocs.Where(x => x.NgayBatDauSuDung.Month == month && x.NgayBatDauSuDung.Year == year).ToListAsync();
            return _mapper.Map<IEnumerable<DichVuNuocDto>>(dsDVNuoc);
        }

        public async Task<IEnumerable<DichVuNuocDto>> GetDVNuocByMonthYearAndMaDH(int month, int year, int maDH)
        {
            var dsDVNuoc = await _context.dvNuocs.Where(x => x.NgayBatDauSuDung.Month == month && x.NgayBatDauSuDung.Year == year && x.MaDH == maDH).ToListAsync();
            return _mapper.Map<IEnumerable<DichVuNuocDto>>(dsDVNuoc);
        }
    }
   
}
