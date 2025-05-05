using BuildingManagement.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface IKhoiNhaService
    {
        Task<KhoiNhaDto> CreateKhoiNha(CreateKhoiNhaDto request, string tennv);
        Task<IEnumerable<KhoiNhaDto>> GetKhoiNhaByMaTN(int matn);
        
    }
}
