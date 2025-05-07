using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services
{
    public class DichVuNuocDinhMucService : IDichVuNuocDinhMucService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DichVuNuocDinhMucService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
