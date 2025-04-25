using System;
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

        // Navigation 
        public tnKhoiNha tnKhoiNha { get; set; }

        public ICollection<tnMatBang> tnMatBangs { get; set; }
    }
}
