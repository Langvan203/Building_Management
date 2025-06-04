using BuildingManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface INhanVienService
    {
        Task<List<GetDSNhanVienDto>> GetDSNhanVien();
        Task<CreateNhanVienDto> CreateNewNhanVien(CreateNhanVienDto dto, string tennv);
        Task<bool> ThemNhanVienToToaNha(int manv, int MaTN);
        Task<bool> XoaNhanVienToaNha(int manv, int MaTN);
        Task<bool> ThemNhanVienPhongBan(int manv, int MaPB);
        Task<bool> XoaNhanVienPhongBan(int manv, int MaPB);
        Task<bool> UpdateThongTinNhanVien(UpdateThongTinNhanVien dto, string tennv);
        Task<bool> UpdatePhongBanNhanVien(List<int> dsPhongBan, int maNV);
        Task<bool> UpdateToaNhaNhanVien(List<int> dsToaNha, int maNV);
        Task<bool> UpdateRoleNhanVien(List<int> dsRole, int maNV);
        Task<bool> RemoveNhanVien(int MaNV);
    }
}
