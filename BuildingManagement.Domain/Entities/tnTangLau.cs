﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class tnTangLau : BaseEntity
    {
        [Key]
        public int MaTL { get; set; }
        public string TenTL { get; set; }
        public decimal DienTichSan { get; set; }
        public decimal DienTichKhuVucDungChung { get; set; }
        public decimal DienTichKyThuaPhuTro { get; set; }

        //FK
        public int MaKN { get; set; }
        public int MaTN { get; set; }

        // Navigation 
        public tnKhoiNha tnKhoiNha { get; set; }
        public tnToaNha tnToaNha { get; set; }

        public ICollection<tnMatBang> tnMatBangs { get; set; }
        public ICollection<tnKhachHang> tnKhachHangs { get; set; }
        public ICollection<dvDichVuSuDung> dvDichVuSuDungs { get; set; }
        public ICollection<dvHoaDon> dvHoaDons { get; set; }
        public ICollection<dvNuocDongHo> dvNuocDongHos { get; set; }
        public ICollection<dvDienDongHo> dvDienDongHos { get; set; }
        public ICollection<tnycYeuCauSuaChua> tnycYeuCauSuaChuas { get; set; }

    }
}
