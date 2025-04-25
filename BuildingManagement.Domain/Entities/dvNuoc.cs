using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class dvNuoc : BaseEntity
    {
        [Key]
        public int MaNuoc { get; set; }
        public decimal ChiSoDau { get; set; }
        public decimal ChiSoCuoi { get; set; }
        public decimal SoTieuThu { get; set; }
        public decimal ThanhTien { get; set; }
        public bool IsThanhToan { get; set; }

        //FK
        public int MaDM { get; set; }

        //Navigation 
        public dvNuocDinhMuc dvNuocDinhMuc { get; set; }    
        public dvNuocDongHo dvNuocDongHo { get; set; }
    }
}
