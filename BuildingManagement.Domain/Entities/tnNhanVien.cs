using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class tnNhanVien : BaseEntity
    {
        [Key]
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChiThuongTru { get; set; }

        // Navigation
        public ICollection<Role> Roles { get; set; }

        // Navigation
        public ICollection<tnPhongBan> tnPhongBans { get; set; }

        public ICollection<tnToaNha> tnToaNhas { get; set; }
    }
}
