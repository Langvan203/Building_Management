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
