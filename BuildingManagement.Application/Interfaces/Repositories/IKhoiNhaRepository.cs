using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IKhoiNhaRepository : IRepository<tnKhoiNha>
    {
        Task<IEnumerable<KhoiNhaDto>> GetDSKhoiNhaByMaTN(int matn);
        Task<List<KhoiNhaDto>> GetDSKhoiNhaDetail();
        Task<List<KhoiNhaFilter>> GetKhoiNhaFilter();
    }
}
