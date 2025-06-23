using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class tnbtHeThong : BaseEntity
    {
        [Key]
        public int MaHeThong { get; set; }
        public string TenHeThong { get; set; }
        public string NhanHieu { get; set; }
        public string Model { get; set; }
        public int TrangThai { get; set; }
        public string SerialNumber { get; set; }
        public string GhiChu { get; set; }
        //FK
        public int? MaMB { get; set; }
        public int? MaTN { get; set; }

        //Navigation
        public tnMatBang tnMatBang { get; set; }
        public tnToaNha tnToaNha { get; set; }
        public ICollection<nkbtKeHoachBaoTri> nkbtKeHoachBaoTris { get; set; }
        public ICollection<nkbtLichSuBaoTri> nkbtLichSuBaoTris { get; set; }
        public ICollection<tnycYeuCauSuaChua> tnycYeuCauSuaChua { get; set; }
    }
}
