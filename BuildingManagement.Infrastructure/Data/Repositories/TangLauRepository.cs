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

        public async Task<List<TangLauDto>> GetDSTangLau()
        {
            var tnTangLau = await _context.tnTangLaus.Include(x => x.tnMatBangs)
                                                            .ThenInclude(x => x.mbTrangThai)
                                                        .Include(x => x.tnMatBangs)
                                                            .ThenInclude(x => x.mbLoaiMB)
                                                        .AsSplitQuery().ToListAsync();
                                            
            var dsTangLau = tnTangLau.Select(x => new TangLauDto
            {
                MaKN = x.MaKN,
                MaTN = x.MaTN,
                TenTL = x.TenTL,
                MaTL = x.MaTL,
                DienTichSan = x.DienTichSan,
                DienTichKhuVucDungChung = x.DienTichKhuVucDungChung,
                DienTichKyThuaPhuTro = x.DienTichKyThuaPhuTro,
                listMatBangInTanLaus = x.tnMatBangs.Select(x => new ListMatBangInTanLau
                {
                    Id = x.MaMB,
                    Number = x.MaVT,
                    FloorId = x.MaTL,
                    Area = x.DienTichBG,
                    Status = x.mbTrangThai.TenTrangThai,
                    Type = x.mbLoaiMB.TenLMB,
                }).ToList()
            }).ToList();
            return dsTangLau;
        }

        public async Task<List<TangLauDto>> GetTangLauByKhoiNha(int MaKN)
        {
            var dsTL = await _context.tnTangLaus.Where(x => x.MaKN == MaKN).ToListAsync();
            var dsTangLau = dsTL.Select(x => new TangLauDto
            {
                MaKN = x.MaKN,
                MaTN = x.MaTN,
                TenTL = x.TenTL,
                MaTL = x.MaTL,
                DienTichSan = x.DienTichSan,
                DienTichKhuVucDungChung = x.DienTichKhuVucDungChung,
                DienTichKyThuaPhuTro = x.DienTichKyThuaPhuTro,
            }).ToList();
            return dsTangLau;
        }

        public async Task<List<TangLauFilter>> GetTangLauFilter()
        {
            var dsTangLau = await _context.tnTangLaus.Select(x => new TangLauFilter
            {
                MaTL = x.MaTL,
                MaKN = x.MaKN,
                TenTL = x.TenTL
            }).ToListAsync();
            return dsTangLau;
        }
    }
}
