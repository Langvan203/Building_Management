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
        Task<IEnumerable<tnTangLau>> GetTangLauByMaTN(int MaTN);
    }
}
