using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Services
{
    public class NKBTChiTietBaoTriService : INKBTChiTietBaoTriService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NKBTChiTietBaoTriService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
