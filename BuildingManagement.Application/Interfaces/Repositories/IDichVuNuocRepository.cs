using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IDichVuNuocRepository : IRepository<dvNuoc>
    {
        Task<IEnumerable<DichVuNuocDto>> GetDVNuocByMonthYearAndMaDH(int month, int year, int maDH);
        Task<IEnumerable<DichVuNuocDto>> GetDVNuocByMonthAndYear(int month, int year);
    }
}
