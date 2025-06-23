using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class nkbtLichSuBaoTri : BaseEntity
    {
        [Key]
        public int MaLichSu { get; set; }
        public string TieuDe { get; set; }
        public string GhiChu { get; set; }
        // FK
        public int MaHeThong { get; set; }
        public int MaKeHoach { get; set; }
        //Navigation 
        public tnbtHeThong tnbtHeThong { get; set; }
        public nkbtKeHoachBaoTri nkbtKeHoachBaoTri { get; set; }
    }
}
