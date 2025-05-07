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
        IKhoiNhaRepository KhoiNhas { get; }
        IMatBangLoaiMatBangRepository LoaiMatBangs { get; }
        IMatBangTrangThaiRepository TrangThaiMatBangs { get; }
        IDichVuLoaiDichVuRepository LoaiDichVus { get; }
        IDichVuSuDungRepository DichVuSuDungs { get; }
        IDichVuRepository DichVus { get; }
        INKBTChiTietBaoTriRepository ChiTietBaoTris { get; }
        INKBTHeThongRepository HeThongs { get; }
        INKBTKeHoachBaoTriRepository KeHoachBaoTris { get; }
        INKBTLichSuBaoTriRepository LichSuBaoTris { get; }
        INKBTTrangThaiBaoTriRepository TrangThaiBaoTris { get; }
        IYeuCauBaoTriRepository YeuCauBaoTris { get; }
        IKhachHangRepository KhachHangs { get; }
        IDichVuDienDinhMucRepository DienDinhMucs { get; }
        IDichVuNuocDinhMucRepository NuocDinhMucs { get; }
        IDichVuNuocRepository Nuocs { get; }
        IDichVuDienRepository Diens { get; }
        IDichVuDienDongHoRepository DienDongHos { get; }
        IDichVuNuocDongHoRepository NuocDongHos { get; }
        IDichVuGuiXeLoaiXeRepository LoaiXes { get; }
        IDichVuGuiXeTheXeRepository TheXes { get; }
        IDichVuHoaDonRepository HoaDons { get; }
        IPhieuThuRepository PhieuThus { get; }
        IPhongBanRepository PhongBans { get; }
        IMatBangRepository MatBangs { get; }
        IRoleRepository Roles { get; }


        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackAsync();
    }
}
