using AutoMapper;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.DTOs.Request.AuthDto;
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
    public class KhachHangRepository : Repository<tnKhachHang>, IKhachHangRepository
    {
        private readonly IMapper _mapper;
        public KhachHangRepository(BuildingManagementDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<KhachHangFilter>> GetDSKhachHang()
        {
            var dsKH = await _context.tnKhachHangs.Select(x => new KhachHangFilter
            {
                Id = x.MaKH,
                Name = x.IsCaNhan == true ? x.HoTen : x.CtyTen,
                Contract = x.DienThoai,
            }).ToListAsync();
            return dsKH;
        }

        public async Task<KhachHangDto> GetKhachHangInfo(LoginDto dto)
        {
            var kh = await _context.tnKhachHangs.Where(x => x.Email == dto.Email && x.MatKhauMaHoa == dto.Password).Include(x => x.tnToaNha).ThenInclude(x => x.tnKhoiNhas)
                .ThenInclude(x => x.tnTangLaus)
                .Include(x => x.tnMatBangs)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            var khInfor = new KhachHangDto
            {
                MaKH = kh.MaKH,
                HoTen = kh.HoTen,
                CtyTen = kh.CtyTen,
                TaiKhoanCuDan = kh.TaiKhoanCuDan,
                DienThoai = kh.DienThoai,
                Email = kh.Email,
                IsCaNhan = kh.IsCaNhan,
                MatKhauMaHoa = kh.MatKhauMaHoa,
                DiaChiThuongTru = kh.DiaChiThuongTru,
                QuocTich = kh.QuocTich,
                MaSoThue = kh.MaSoThue,
                CCCD = kh.CCCD,
                NgayCap = kh.NgayCap,
                NoiCap = kh.NoiCap,
                GioiTinh = kh.GioiTinh,
                TenTN = kh.tnToaNha?.TenTN,
                MaTN = kh.tnToaNha?.MaTN ?? 0,
                MaKN = kh.tnKhoiNha?.MaKN ?? 0,
                TenKN = kh.tnKhoiNha?.TenKN,
                MaTL = kh.tnTangLau?.MaTL ?? 0,
                TenTL = kh.tnTangLau?.TenTL,
                matBangSoHuus = kh.tnMatBangs.Select(mb => new MatBangSoHuu
                {
                    MaMB = mb.MaMB,
                    MaVT = mb.MaVT,
                    DienTichBanGiao = mb.DienTichBG
                }).ToList() ?? new List<MatBangSoHuu>()
            };
            return khInfor;
        }

        
    }
    
}
