using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface IMatBangRepository : IRepository<tnMatBang>
    {
        Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTL(int MaTL, int MaTN);
        Task<IEnumerable<MatBangDto>> GetDSMatBangByMaKH(int MaKH);
        Task<IEnumerable<MatBangDto>> GetDSMatBangByMaLMB(int MaLMB, int MaTN);
        Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTT(int MaTT, int MaTN);
        Task<IEnumerable<MatBangDto>> GetDSMatBangByMaTN(int MaTN);
        Task<List<DanhSachMatBangDTO>> GetDSMatBang();
    }
}
