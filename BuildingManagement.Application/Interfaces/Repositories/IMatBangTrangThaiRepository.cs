using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IMatBangTrangThaiRepository : IRepository<mbTrangThai>
    {
        Task<IEnumerable<TrangThaiMatBangDto>> GetDSTrangThai();
    }
}
