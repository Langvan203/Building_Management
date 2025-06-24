using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IDichVuDienRepository : IRepository<dvDien>
    {
        Task<IEnumerable<DichVuDienDto>> GetAllDienByThangNam(int thang, int nam);
        Task<IEnumerable<DichVuDienDto>> GetAllDienByThangNamAndMaDH(int thang, int nam, int MaDH);
    }
}
