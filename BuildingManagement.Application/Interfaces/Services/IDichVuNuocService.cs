using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IDichVuNuocService
    {
        Task<IEnumerable<DichVuNuocDto>> GetDVNuocByMonthAndYear(int month, int year);
        Task<IEnumerable<DichVuNuocDto>> GetDVNuocByMonthYearAndMaDH(int month, int year, int MaDH);
        Task<DichVuNuocDto> CreateNewSDNuoc(CreateDichVuNuocDto dto, string name);
    }
}
