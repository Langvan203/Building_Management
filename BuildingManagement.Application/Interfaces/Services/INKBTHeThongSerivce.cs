using BuildingManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface INKBTHeThongSerivce
    {
        Task<PagedResult<HeThongDTO>> GetDSHeThong(int pageNumber, int pageSize = 15);
        Task<bool> UpdateHeThong(UpdateHeThongDto updateHeThongDto, string Name);
        Task<bool> DeleteHeThong(int MaHeThong);
        Task<CreateHeThong> CreteNewHeThong(CreateHeThong createHeThong, string Name);
    }
}
