using AutoMapper;
using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services
{
    public class MatBangService : IMatBangService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MatBangService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MatBangDto> BanGiaoMatBang(int MaMB, int MaKH)
        {
            var checkMB = await _unitOfWork.MatBangs.GetFirstOrDefaultAsync(x => x.MaMB == MaMB);
            if (checkMB != null)
            {
                checkMB.MaKH = MaKH;
                checkMB.MaTrangThai = 2;
                await _unitOfWork.MatBangs.UpdateAsync(checkMB);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<MatBangDto>(checkMB);
            }
            return null;
        }

        public async Task<MatBangDto> CreateMatBang(CreateMatBangDto dto, string name)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var checkTTMB = await _unitOfWork.MatBangs.GetFirstOrDefaultAsync(x => x.MaVT == dto.MaVT && x.MaKN == dto.MaKN && x.MaTL == dto.MaTL && x.MaTN == dto.MaTN);
                if (checkTTMB == null)
                {
                    var newMB = _mapper.Map<tnMatBang>(dto);
                    newMB.NguoiTao = name;
                    await _unitOfWork.MatBangs.AddAsync(newMB);
                    await _unitOfWork.SaveChangesAsync();

                    // thêm dịch vụ phí quản lý
                    var dvPQL = await _unitOfWork.DichVus.GetFirstOrDefaultAsync(x => x.MaDV == 8);

                    var dvSuDung = new dvDichVuSuDung
                    {
                        NgayBatDauTinhPhi = DateTime.Now,
                        NgayKetThucTinhPhi = DateTime.Now.AddMonths(1),
                        IsDuyet = false,
                        ThanhTien = newMB.DienTichBG * dvPQL.DonGia + newMB.DienTichBG * dvPQL.DonGia * dvPQL.TyLeVAT + dvPQL.DonGia * dvPQL.TyLeBVMT,
                        GhiChu = "Thêm phí quản lý tự động",
                        MaDV = dvPQL.MaDV,
                        MaKH = newMB.MaTrangThai == 2 ? newMB.MaKH : null,
                        MaMB = newMB.MaMB,
                        NguoiTao = newMB.NguoiTao,
                        MaKN = newMB.MaKN,
                        MaTL = newMB.MaTL,
                        MaTN = newMB.MaTN,
                        TienBVMT = newMB.DienTichBG * dvPQL.DonGia * dvPQL.TyLeBVMT/100,
                        TienVAT = newMB.DienTichBG * dvPQL.DonGia * dvPQL.TyLeVAT/100,
                    };
                    await _unitOfWork.DichVuSuDungs.AddAsync(dvSuDung);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();

                    return _mapper.Map<MatBangDto>(newMB);
                }
                return null;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Lỗi thêm mặt bằng", ex);
            }
        }

        public async Task<List<DanhSachMatBangDTO>> GetDSMatBang()
        {
            var dsMatBang = await _unitOfWork.MatBangs.GetDSMatBang();
            if (dsMatBang == null)
                return null;
            return dsMatBang;
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaKH(int MaKH)
        {
            var dsMB = await _unitOfWork.MatBangs.GetDSMatBangByMaKH(MaKH);
            return dsMB;
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaLMB(int MaLMB, int MaTN)
        {
            var dsMB = await _unitOfWork.MatBangs.GetDSMatBangByMaLMB(MaLMB,MaTN);
            return dsMB;
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTL(int MaTL, int MaTN)
        {
            var dsMB = await _unitOfWork.MatBangs.GetDSMatBangByMaTL(MaTL, MaTN);
            return dsMB;
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTN(int MaTN)
        {
            var dsMB = await _unitOfWork.MatBangs.GetDSMatBangByMaTN(MaTN);
            return dsMB;
        }

        public async Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTT(int MaTT, int MaTN)
        {
            var dsMB = await _unitOfWork.MatBangs.GetDSMatBangByMaTT(MaTT, MaTN);
            return dsMB;
        }

        public async Task<bool> RemoveMatBang(int MaMB)
        {
            var checkMB = await _unitOfWork.MatBangs.GetFirstOrDefaultAsync(x => x.MaMB == MaMB);
            if(checkMB != null)
            {
                await _unitOfWork.MatBangs.DeleteAsync(checkMB);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<MatBangDto> UpdateMatBang(UpdateThongTinCoBanMatBangDto dto, string name)
        {
            var checkMB = await _unitOfWork.MatBangs.GetFirstOrDefaultAsync(x => x.MaMB == dto.MaMB);
            if(checkMB != null)
            {
                checkMB.DienTichThongThuy = dto.DienTichThongThuy;
                checkMB.MaTrangThai = dto.MaTrangThai;
                checkMB.MaKH = dto.MaKhachHang;
                checkMB.DienTichTimTuong = dto.DienTichTimTuong;
                checkMB.NgayBanGiao = dto.NgayBanGiao;
                checkMB.NgayHetHanChoThue = dto.NgayHetHanChoThue;
                checkMB.DienTichBG = dto.DienTichBG;
                checkMB.NguoiSua = name;
                await _unitOfWork.MatBangs.UpdateAsync(checkMB);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<MatBangDto>(checkMB);
            }
            return null;
        }
    }
}
