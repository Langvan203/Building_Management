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

        //FK
        public int MaHeThong { get; set; }
        public int MaTrangThai { get; set; }

        //Navigation
        public tnbtHeThong tnbtHeThong { get; set; }

        public nkbtTrangThai nkbtTrangThai { get; set; }
        public ICollection<nkbtChiTietBaoTri> nkbtChiTietBaoTris { get; set; }
        public ICollection<tnNhanVien> tnNhanViens { get; set; }
    }
}
