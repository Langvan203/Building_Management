using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IToaNhaServices
    {
        Task<IEnumerable<ToaNhaDto>> GetDSToaNhaAsync();
        Task<ToaNhaDto> GetToaNhaTheoIdAsync(int id);
        Task<tnToaNha> TaoToaNhaAsync(CreateToaNhaDto dto, string tennv);
        Task<bool> XoaToaNhaAsync(int id);
    }
}
