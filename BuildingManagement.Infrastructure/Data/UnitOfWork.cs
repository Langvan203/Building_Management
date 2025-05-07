using AutoMapper;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using BuildingManagement.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BuildingManagementDbContext _context;
        private readonly ToaNhaRepository _toanha;
        private readonly TangLauRepository _tanglau;
        private readonly NhanVienRepository _nhanVien;
        private readonly LoaiMatBangRepository _loaiMatBang;
        private readonly TrangThaiMatBangRepository _trangThaiMatBang;
        private readonly MatBangRepository _matBang;
        private readonly KhoiNhaRepository _khoiNha;
        private readonly LoaiDichVuRepository _loaiDichVu;
        private readonly RoleRepository _role;

        private readonly IDichVuRepository _dichVu;
        private readonly IDichVuDienDongHoRepository _dienDongHo;
        private readonly IDichVuNuocDongHoRepository _nuocDongHo;
        private readonly IDichVuGuiXeTheXeRepository _theXe;
        private readonly IDichVuGuiXeLoaiXeRepository _loaiXe;
        private readonly IDichVuNuocRepository _nuoc;
        private readonly IDichVuDienRepository _dien;
        private readonly IDichVuNuocDinhMucRepository _nuocDinhMuc;
        private readonly IDichVuDienDinhMucRepository _dienDinhMuc;
        private readonly IDichVuSuDungRepository _dichVuSuDung;
        private readonly IDichVuHoaDonRepository _hoaDon;
        private readonly IKhachHangRepository _khachHang;
        private readonly IPhongBanRepository _phongBan;
        private readonly IPhieuThuRepository _phieuThu;
        private readonly INKBTChiTietBaoTriRepository _chiTietBaoTri;
        private readonly INKBTHeThongRepository _heThong;
        private readonly INKBTKeHoachBaoTriRepository _keHoachBaoTri;
        private readonly INKBTLichSuBaoTriRepository _lichSuBaoTri;
        private readonly INKBTTrangThaiBaoTriRepository _trangThaiBaoTri;
        private readonly IYeuCauBaoTriRepository _yeuCauBaoTri;
        private IDbContextTransaction _transaction;

        private readonly IMapper _mapper;

        public IToaNhaRepository ToaNhas => _toanha;

        public ITangLauRepository TangLaus => _tanglau;

        public INhanVienRepository NhanViens => _nhanVien;

        public IKhoiNhaRepository KhoiNhas => _khoiNha;

        public IDichVuLoaiDichVuRepository LoaiDichVus => _loaiDichVu;

        public IMatBangLoaiMatBangRepository LoaiMatBangs => _loaiMatBang;

        public IMatBangRepository MatBangs => _matBang;

        public IMatBangTrangThaiRepository TrangThaiMatBangs => _trangThaiMatBang;
        public IRoleRepository Roles => _role;

        public IDichVuSuDungRepository DichVuSuDungs => _dichVuSuDung;

        public IDichVuRepository DichVus => _dichVu;

        public INKBTChiTietBaoTriRepository ChiTietBaoTris => _chiTietBaoTri;

        public INKBTHeThongRepository HeThongs => _heThong;

        public INKBTKeHoachBaoTriRepository KeHoachBaoTris => _keHoachBaoTri;

        public INKBTLichSuBaoTriRepository LichSuBaoTris => _lichSuBaoTri;

        public INKBTTrangThaiBaoTriRepository TrangThaiBaoTris => _trangThaiBaoTri;

        public IYeuCauBaoTriRepository YeuCauBaoTris => _yeuCauBaoTri;

        public IKhachHangRepository KhachHangs => _khachHang;

        public IDichVuDienDinhMucRepository DienDinhMucs => _dienDinhMuc;

        public IDichVuNuocDinhMucRepository NuocDinhMucs => _nuocDinhMuc;

        public IDichVuNuocRepository Nuocs => _nuoc;

        public IDichVuDienRepository Diens => _dien;

        public IDichVuDienDongHoRepository DienDongHos => _dienDongHo;

        public IDichVuNuocDongHoRepository NuocDongHos => _nuocDongHo;

        public IDichVuGuiXeLoaiXeRepository LoaiXes => _loaiXe;

        public IDichVuGuiXeTheXeRepository TheXes => _theXe;

        public IDichVuHoaDonRepository HoaDons => _hoaDon;

        public IPhieuThuRepository PhieuThus => _phieuThu;

        public IPhongBanRepository PhongBans => _phongBan;

        public UnitOfWork(BuildingManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _toanha = new ToaNhaRepository(_context);
            _tanglau = new TangLauRepository(_context, mapper);
            _nhanVien = new NhanVienRepository(_context);
            _khoiNha = new KhoiNhaRepository(_context, mapper);
            _trangThaiMatBang = new TrangThaiMatBangRepository(_context, mapper);
            _loaiMatBang = new LoaiMatBangRepository(_context, mapper);
            _matBang = new MatBangRepository(_context, mapper);
            _loaiDichVu = new LoaiDichVuRepository(_context);
            _dichVu = new DichVuRepository(_context);
            _dienDongHo = new DichVuDienDongHoRepository(_context);
            _nuocDongHo = new DichVuNuocDongHoRepository(_context);
            _theXe = new DichVuGuiXeTheXeRepository(_context, mapper);
            _loaiXe = new DichVuGuiXeLoaiXeRepository(_context);
            _nuoc = new DichVuNuocRepository(_context);
            _dien = new DichVuDienRepository(_context);
            _nuocDinhMuc = new DichVuNuocDinhMucRepository(_context);
            _dienDinhMuc = new DichVuDienDinhMucRepository(_context);
            _dichVuSuDung = new DichVuSuDungRepository(_context);
            _hoaDon = new DichVuHoaDonRepository(_context);
            _khachHang = new KhachHangRepository(_context);
            _phongBan = new PhongBanRepository(_context);
            _phieuThu = new PhieuThuRepository(_context);
            _chiTietBaoTri = new NKBTChiTietBaoTriRepository(_context);
            _heThong = new NKBTHeThongRepository(_context);
            _keHoachBaoTri = new NKBTKeHoachBaoTriRepository(_context);
            _lichSuBaoTri = new NKBTLichSuBaoTriRepository(_context);
            _trangThaiBaoTri = new NKBTTrangThaiBaoTriRepository(_context);
            _yeuCauBaoTri = new YeuCauBaoTriRepository(_context);
            _role = new RoleRepository(_context);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
