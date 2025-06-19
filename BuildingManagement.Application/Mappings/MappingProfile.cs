using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BuildingManagement.Application.DTOs;
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

            CreateMap<dvDienDinhMuc, DichVuDienDinhMucDto>();
            CreateMap<CreateDinhMuc, dvDienDinhMuc>();

            CreateMap<dvDienDongHo, DongHoDTO>();
            CreateMap<CreateDongHoDto, dvDienDongHo>();

            CreateMap<dvgxLoaiXe, DichVuGuiXeLoaiXeDto>();
            CreateMap<CreateDichVuGuiXeLoaiXeDto, dvgxLoaiXe>();

            CreateMap<dvgxTheXe, DichVuGuiXeTheXeDto>();
            CreateMap<CreateDichVuGuiXeTheXeDto, dvgxTheXe>();

            CreateMap<dvDien, DichVuDienDto>();
            CreateMap<CreateDichVuDienDto, dvDien>();

            CreateMap<dvNuocDinhMuc, DichVuNuocDinhMucDto>();
            CreateMap<CreateDinhMuc, dvNuocDinhMuc>();

            CreateMap<dvNuoc, DichVuNuocDto>();
            CreateMap<CreateDichVuNuocDto, dvNuoc>();

            CreateMap<dvDichVuSuDung, DichVuSuDungDto>();
            CreateMap<CreateDichVuSuDungDto, dvDichVuSuDung>();

            CreateMap<tnKhachHang, KhachHangDto>();
            CreateMap<CreateKhachHangDto, tnKhachHang>();

            CreateMap<CreateDichVuDto, dvDichVu>();
            CreateMap<dvDichVu, DichVuDto>();

            CreateMap<tnPhongBan, PhongBanDto>();
            CreateMap<CreatePhongBanDto, tnPhongBan>();

            CreateMap<CreateNhanVienDto, tnNhanVien>();
        }
    }
}
