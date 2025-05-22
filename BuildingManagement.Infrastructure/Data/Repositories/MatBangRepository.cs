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

        public async Task<List<DanhSachMatBangDTO>> GetDSMatBang()
        {
            var dsMatBang = await _context.tnMatBangs.Include(x => x.mbLoaiMB)
                                                        .Include(x => x.mbTrangThai)
                                                        .Include(x => x.tnKhachHang)
                                                    .Include(x => x.tnToaNha)
                                                    .Include(x => x.tnTangLau)
                                                    .Include(x => x.tnKhoiNha)
                                                        .AsSplitQuery().ToListAsync();
            var dsMatBangDto = dsMatBang.Select(x => new DanhSachMatBangDTO
            {
                MaMB = x.MaMB,
                MaTN = x.MaTN,
                MaKN = x.MaKN == null ? 0 : (int)x.MaKN,
                MaTL = x.MaTL,
                MaVT = x.MaVT,
                DienTichBG = x.DienTichBG,
                DienTichThongThuy = x.DienTichThongThuy,
                DienTichTimTuong = x.DienTichTimTuong,
                SoHopDong = x.SoHopDong,
                NgayBanGiao = x.NgayBanGiao,
                NgayHetHanChoThue = x.NgayHetHanChoThue,
                MaLMB = x.MaLMB,
                TenLMB = x.mbLoaiMB.TenLMB,
                MaKH = x.MaKH == null ? 0 : (int)x.MaKH,
                TenKH = x.tnKhachHang == null ? "" : x.tnKhachHang.IsCaNhan == true ? x.tnKhachHang.HoTen : x.tnKhachHang.CtyTen,
                MaTT = x.MaTrangThai,
                TenTrangThai = x.mbTrangThai.TenTrangThai,
                TenTN = x.tnToaNha.TenTN,
                TenKN = x.tnKhoiNha.TenKN,
                TenTL = x.tnTangLau.TenTL
            }).ToList();
            return dsMatBangDto;
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
