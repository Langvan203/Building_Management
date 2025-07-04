﻿using BuildingManagement.Application.DTOs;
using BuildingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Repositories
{
    public interface INKBTTrangThaiBaoTriRepository : IRepository<nkbtTrangThai>
    {
        Task<List<TrangThaiBaoTriDto>> GetDSTrangThai();
        Task<List<TrangThaiBaoTriDto>> GetDSTrangThaiYeuCau();
    }
}
