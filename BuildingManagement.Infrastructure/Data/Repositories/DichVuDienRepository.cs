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
    public class DichVuDienRepository : Repository<dvDien>, IDichVuDienRepository
    {
        private readonly IMapper _mapper;
        public DichVuDienRepository(BuildingManagementDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<DichVuDienDto>> GetAllDienByThangNam(int thang, int nam)
        {
            var dsSuDungDien = await _context.dvDiens.Where(x => x.NgayBatDauSuDung.Month == thang && x.NgayBatDauSuDung.Year == nam).ToListAsync();
            return _mapper.Map<IEnumerable<DichVuDienDto>>(dsSuDungDien);
        }

        public async Task<IEnumerable<DichVuDienDto>> GetAllDienByThangNamAndMaDH(int thang, int nam, int MaDH)
        {
            var dsSuDungDien = await _context.dvDiens.Where(x => x.NgayBatDauSuDung.Month == thang && x.NgayBatDauSuDung.Year == nam && x.MaDH == MaDH).ToListAsync();
            return _mapper.Map<IEnumerable<DichVuDienDto>>(dsSuDungDien);
        }
    }
    
}
