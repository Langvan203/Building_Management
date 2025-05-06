using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BuildingManagement.Application.DTOs.Request;
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
            CreateMap<tnKhoiNha, KhoiNhaDto>();
            CreateMap<CreateKhoiNhaDto, tnKhoiNha>();

            CreateMap<tnTangLau, TangLauDto>();
            CreateMap<CreateTangLauDto, tnTangLau>();

            CreateMap<mbLoaiMB, LoaiMatBangDto>();
            CreateMap<CreateNewLoaiMB, mbLoaiMB>();

            CreateMap<CreateNewTrangThaiMatBangDto, mbTrangThai>();
            CreateMap<mbTrangThai, TrangThaiMatBangDto>();

            CreateMap<tnMatBang, MatBangDto>();
            CreateMap<CreateMatBangDto, tnMatBang>();

            CreateMap<dvLoaiDV, LoaiDVDto>();
            CreateMap<CreateLoaiDVDto, dvLoaiDV>();
        }
    }
}
