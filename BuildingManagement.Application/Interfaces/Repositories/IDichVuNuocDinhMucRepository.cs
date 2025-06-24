using BuildingManagement.Application.DTOs;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IDichVuNuocDinhMucRepository : IRepository<dvNuocDinhMuc>
    {
        Task<List<DinhMucDTO>> GetDSNuocDinhMuc();
        Task<dvNuocDinhMuc> CheckDinhMuc(CreateDinhMuc dto);
        Task<dvNuocDinhMuc> CheckByID(int MaDM);
    }
}
