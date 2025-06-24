using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface ITangLauServices
    {
        Task<TangLauDto> CreateTangLau(CreateTangLauDto tangLauDto,string TenNguoiTao);
        Task<IEnumerable<TangLauDto>> GetDSTangLauByMaKN(int MaKN, int MaTN);
        Task<List<TangLauDto>> GetDSTangLau();
        Task<List<TangLauFilter>> GetTangLauFilter();
        Task<bool> UpdateTangLau(UpdateTangLauDto tangLauDto, string tennv);
        Task<bool> DeleteTangLau(int MaTL);
        Task<List<TangLauDto>> GetDSTangLauByMaKN(int MaKN);
    }
}
