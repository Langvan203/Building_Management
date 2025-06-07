using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.DTOs.Request
{
    public class LoaiDVDto
    {
        public int MaLDV { get; set; }
        public string TenLDV { get; set; }
    }

    public class CreateLoaiDVDto
    {
        public string TenLDV { get; set; }
        public string? Icon { get; set; }
        public string? MoTa { get; set; }
        public int? MaTN { get; set; }
        public bool IsEssential { get; set; } = false;
    }

    public class GetDSLoaiDichVu
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isEssential { get; set; }
        public int servicesCount { get; set; }
        public string icon { get; set; }
        public string description { get; set; }
        public int matn { get; set; }
    }

}
