using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IPhongBanService
    {
        Task<List<PhongBanDto>> GetAllPhongBan();
        Task<PhongBanDto> GetPhongBanById(int id);
        Task<CreatePhongBanDto> CreatePhongBanDto(CreatePhongBanDto dto, string tennv);
        Task<bool> RemovePhongBan(int id);
        Task<bool> UpdatePhongBan(UpdateDatePhongBanDto dto,string tennv);
        Task<bool> RemoveNhanVienInPhongBan(int maPB, int maNV);
    }
}
