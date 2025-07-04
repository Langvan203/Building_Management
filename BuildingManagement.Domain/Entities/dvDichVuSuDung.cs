﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class dvDichVuSuDung : BaseEntity
    {
        [Key]
        public int MaDVSD { get; set; }
        public DateTime NgayBatDauTinhPhi { get; set; }
        public DateTime NgayKetThucTinhPhi { get; set; }
        public decimal TienVAT { get; set; }
        public int IsDuyet { get; set; }
        public decimal TienBVMT { get; set; }
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }
        public bool? IsRequestByResident { get; set; }
        public bool? IsChuyenHoaDon { get; set; }
        public bool? TrangThaiSuDung { get; set; }
        // FK
        public int MaDV { get; set; }
        public int? MaKH { get; set; }
        public int MaMB { get; set; }
        public int? MaTN { get; set; }
        public int? MaKN { get; set; }
        public int? MaTL { get; set; }
        //Navigation 
        public dvDichVu dvDichVu { get; set; }
        public ICollection<dvHoaDon> dvHoaDons { get; set; }
        public tnKhachHang tnKhachHang { get; set; }
        public tnMatBang tnMatBang { get; set; }
        public tnTangLau tnTangLau { get; set; }
        public tnKhoiNha tnKhoiNha { get; set; }
        public tnToaNha tnToaNha { get; set; }
    }
}
