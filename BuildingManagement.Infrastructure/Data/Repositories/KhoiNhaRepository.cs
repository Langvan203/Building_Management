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

        public async Task<List<KhoiNhaDto>> GetDSKhoiNhaDetail()
        {
            try
            {
                var dsToanha = await _context.tnToaNhas.Include(x => x.tnKhoiNhas).ThenInclude(x => x.tnTangLaus).ThenInclude(x => x.tnMatBangs).ToListAsync();

                var khoinha = dsToanha.GroupBy(x => new { x.TenTN, x.MaTN }).Select(x => new KhoiNhaDto
                {
                    MaTN = x.Key.MaTN,
                    TenTN = x.Key.TenTN,
                    KhoiNhaDetail = x.SelectMany(y => y.tnKhoiNhas).Select(z => new KhoiNhaDetailDto
                    {
                        MaKN = z.MaKN,
                        TenKN = z.TenKN,
                        MaTN = z.MaTN,
                        Status = z.TrangThaiKhoiNha ?? 0,
                        TotalFloors = z.tnTangLaus.Count == 0 ? 0 : z.tnTangLaus.Count,
                        TotalPremies = z.tnTangLaus.Count == 0 ? 0 : z.tnTangLaus.SelectMany(x => x.tnMatBangs).Count() == 0 ? 0 : z.tnTangLaus.SelectMany(x => x.tnMatBangs).Count(),
                        OccupancyRate = z.tnTangLaus.Count == 0 ? 0 : z.tnTangLaus.SelectMany(x => x.tnMatBangs).Count() == 0 ? 0 :
                        (decimal)z.tnTangLaus.SelectMany(x => x.tnMatBangs).Count(x => x.MaTrangThai == 2) / z.tnTangLaus.SelectMany(x => x.tnMatBangs).Count() * 100,
                        listTangLauInKhoiNhas = z.tnTangLaus.Select(tl => new ListTangLauInKhoiNha
                        {
                            TenTL = tl.TenTL,
                            TenTN = x.Key.TenTN,
                            TenKN = z.TenKN,
                            DienTichSan = tl.DienTichSan,
                            TotalPremises = tl.tnMatBangs.Count
                        }).ToList()
                    }).ToList()
                }).ToList();
                return khoinha;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<IEnumerable<KhoiNhaDto>> GetDSKhoiNhaByMaTN(int matn)
        {
            var dsToanha = await _context.tnToaNhas.Include(x => x.tnKhoiNhas).AsSplitQuery().ToListAsync();

            var khoinha = dsToanha.GroupBy(x => new {x.TenTN, x.MaTN}).Select(x => new KhoiNhaDto
            {
                MaTN = x.Key.MaTN,
                TenTN = x.Key.TenTN,
                KhoiNhaDetail = x.SelectMany(y => y.tnKhoiNhas).Where(z => z.MaTN == matn).Select(z => new KhoiNhaDetailDto
                {
                    MaKN = z.MaKN,
                    TenKN = z.TenKN,
                    MaTN = z.MaTN,
                    Status = (int)z.TrangThaiKhoiNha
                }).ToList()
            }).ToList();

            return khoinha;
        }

        public async Task<List<KhoiNhaFilter>> GetKhoiNhaFilter()
        {
             var dsKhoiNha = await _context.tnKhoiNhas.Select(x => new KhoiNhaFilter
             {
                 MaKN = x.MaKN,
                 TenKN = x.TenKN,
                 MaTN = x.MaTN
             }).ToListAsync();
            return dsKhoiNha;
        }
    }
}
