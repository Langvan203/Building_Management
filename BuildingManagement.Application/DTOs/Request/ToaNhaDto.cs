using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class ToaNhaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal OccupancyRate { get; set; }
        public int ConstructionYear { get; set; }
        public string Status { get; set; }
        public int SoTangHam { get; set; }
        public int SoTangNoi { get; set; }
        public string SoTaiKhoan { get; set; }
        public string NoiDungChuyenKhoan { get; set; }
        public string NganHangThanhToan { get; set; }
        public decimal DienTichXayDung { get; set; }
        public decimal TongDienTichSan { get; set; }
        public decimal TongDienTichChoThueNET { get; set; }
        public decimal TongDienTichChoThueGross { get; set; }
    }

    public class CreateToaNhaDto
    {
        public string TenTN { get; set; }
        public string DiaChi { get; set; }
        //public decimal OccupancyRate { get; set; }
        //public int ConstructionYear { get; set; }
        public int TrangThaiToaNha { get; set; }
        public int SoTangNoi { get; set; }
        public int SoTangHam { get; set; }
        public string SoTaiKhoan { get; set; }
        public string NoiDungChuyenKhoan { get; set; }
        public string NganHangThanhToan { get; set; }
        public decimal DienTichXayDung { get; set; }
        public decimal TongDienTichSan { get; set; }
        public decimal TongDienTichChoThueNET { get; set; }
        public decimal TongDienTichChoThueGross { get; set; }
    }
    public class UpdateToaNhaDto
    {
        public int Id { get; set; }
        public string TenTN { get; set; }
        public string DiaChi { get; set; }
        //public decimal OccupancyRate { get; set; }
        //public int ConstructionYear { get; set; }
        public int TrangThaiToaNha { get; set; }
        public int SoTangNoi { get; set; }
        public int SoTangHam { get; set; }
        public string SoTaiKhoan { get; set; }
        public string NoiDungChuyenKhoan { get; set; }
        public string NganHangThanhToan { get; set; }
        public decimal DienTichXayDung { get; set; }
        public decimal TongDienTichSan { get; set; }
        public decimal TongDienTichChoThueNET { get; set; }
        public decimal TongDienTichChoThueGross { get; set; }
    }

}
