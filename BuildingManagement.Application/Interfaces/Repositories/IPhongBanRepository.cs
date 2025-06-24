using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IPhongBanRepository : IRepository<tnPhongBan>
    {
        Task<List<PhongBanDto>> GetAllPhongBan();
        Task<tnPhongBan> GetPhongBanById(int id);
        Task<bool> AddPhongBan(tnPhongBan phongBan);
        Task<bool> UpdatePhongBan(tnPhongBan phongBan);
        Task<bool> DeletePhongBan(int id);
        Task<tnNhanVien> CheckNhanVienInPhongBan(int MaPB, int MaNV);
        Task<bool> RemoveNhanVienInPhongBan(int MaPB, int MaNV);
    }
}
