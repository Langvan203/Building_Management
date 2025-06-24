using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class nvDanhGia
    {
        [Key]
        public int MaDanhGia { get; set; }
        public decimal DiemDanhGia { get; set; }
        public string? GhiChu { get; set; }

        //FK
        public int MaNV { get; set; }

        //Navigation
        public tnNhanVien tnNhanVien { get; set; }
    }
}
