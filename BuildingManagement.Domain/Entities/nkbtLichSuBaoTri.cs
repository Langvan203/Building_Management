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
        public string GhiChi { get; set; }

        // FK
        public int MaHeThong { get; set; }
        
        //Navigation 
        public tnbtHeThong tnbtHeThong { get; set; }
    }
}
