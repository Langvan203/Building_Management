using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IDichVuLoaiDichVuRepository : IRepository<dvLoaiDV>
    {
        Task<List<GetDSLoaiDichVu>> GetDSLoaiDichVu();
        Task<List<GetDSLoaiDichVu>> GetDSLoaiDichVuByMaTN(int MaTN);
    }
}
