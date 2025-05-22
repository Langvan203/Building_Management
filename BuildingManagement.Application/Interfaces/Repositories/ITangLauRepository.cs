using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface ITangLauRepository : IRepository<tnTangLau>
    {
        Task<IEnumerable<TangLauDto>> GetDSTangLau(int MaTN, int MaKN);
        Task<bool> CheckTangLau(int MaKN, int MaTN);
        Task<List<TangLauDto>> GetDSTangLau();
        Task<List<TangLauFilter>> GetTangLauFilter();
        Task<List<TangLauDto>> GetTangLauByKhoiNha(int MaKN);
    }
}
