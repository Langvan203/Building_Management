using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IDichVuDienService
    {
        Task<IEnumerable<DichVuDienDto>> GetDVDienByMonthAndYear(int month, int year);
        Task<IEnumerable<DichVuDienDto>> GetDVDienByMonthYearAndMaDH(int month, int year, int MaDH);
        Task<DichVuDienDto> CreateNewSDDien(CreateDichVuDienDto dto, string name);
    }
}
