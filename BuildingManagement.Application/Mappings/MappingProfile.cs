using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BuildingManagement.Application.DTOs;
using BuildingManagement.Application.DTOs.Request.AuthDto;
using BuildingManagement.Domain.Entities;

namespace BuildingManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<tnToaNha, ToaNhaDto>();
            CreateMap<CreateToaNhaDto, tnToaNha>();
            CreateMap<RegisterDto, tnNhanVien>();
        }
    }
}
