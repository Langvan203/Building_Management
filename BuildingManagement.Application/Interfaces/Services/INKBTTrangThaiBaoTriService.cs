﻿using BuildingManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Interfaces.Services
{
    public interface INKBTTrangThaiBaoTriService
    {
        Task<List<TrangThaiBaoTriDto>> GetDSTrangThai();
        Task<List<TrangThaiBaoTriDto>> GetDSTrangThaiYeuCau();
    }
}
