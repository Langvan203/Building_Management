using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs
{
    public class DinhMucDTO
    {
        public int MaDM { get; set; }
        public string TenDM { get; set; }
        public int ChiSoDau { get; set; }
        public int ChiSoCuoi { get; set; }
        public decimal DonGia { get; set; }
        public string Description { get; set; }
    }

    public class CreateDinhMuc
    {
        public string TenDM { get; set; }
        public decimal DonGiaDinhMuc { get; set; }
        public int ChiSoDau { get; set; }
        public int ChiSoCuoi { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateDinhMuc
    {
        public int MaDM { get; set; }
        public string TenDM { get; set; }
        public decimal DonGiaDinhMuc { get; set; }
        public int ChiSoDau { get; set; }
        public int ChiSoCuoi { get; set; }
        public string? Description { get; set; }
    }
}
