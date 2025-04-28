using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IToaNhaRepository ToaNhas { get; }
        ITangLauRepository TangLaus { get; }
        INhanVienRepository NhanViens { get; }
        IRoleRepository Roles { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackAsync();
    }
}
