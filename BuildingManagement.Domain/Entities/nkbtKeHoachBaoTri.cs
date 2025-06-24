using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class nkbtKeHoachBaoTri : BaseEntity
    {
        [Key]
        public int MaKeHoach { get; set; }
        public string TenKeHoach { get; set; }
        public int LoaiBaoTri { get; set; } // 0: Bảo trì định kỳ, 1: Bảo trì đột xuất
        //FK
        public int MaHeThong { get; set; }
        public int MaTrangThai { get; set; }
        public int TanSuat { get; set; }
        public string MoTaCongViec { get; set; }
        public DateTime NgayBaoTri { get; set; }
        //FK
        //Navigation
        public tnbtHeThong tnbtHeThong { get; set; }
        public ICollection<nkbtLichSuBaoTri> nkbtLichSuBaoTris { get; set; }
        public nkbtTrangThai nkbtTrangThai { get; set; }
        public ICollection<nkbtChiTietBaoTri> nkbtChiTietBaoTris { get; set; }
        public ICollection<tnNhanVien> tnNhanViens { get; set; }


    }
}
