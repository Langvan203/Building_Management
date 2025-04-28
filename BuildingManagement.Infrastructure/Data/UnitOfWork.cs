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
        private readonly RoleRepository _role;
        private IDbContextTransaction _transaction;

        public IToaNhaRepository ToaNhas => _toanha;

        public ITangLauRepository TangLaus => _tanglau;

        public INhanVienRepository NhanViens => _nhanVien;

        public IRoleRepository Roles => _role;

        public UnitOfWork(BuildingManagementDbContext context)
        {
            _context = context;
            _toanha = new ToaNhaRepository(_context);
            _tanglau = new TangLauRepository(_context);
            _nhanVien = new NhanVienRepository(_context);
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
