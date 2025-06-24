using BuildingManagement.Application.DTOs;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IDichVuDienDinhMucRepository : IRepository<dvDienDinhMuc>
    {
        Task<List<DinhMucDTO>> GetDSDienDinhMuc();
        Task<dvDienDinhMuc> CheckDinhMuc(CreateDinhMuc dto);
        Task<dvDienDinhMuc> CheckByID(int MaDM);
    }
}
