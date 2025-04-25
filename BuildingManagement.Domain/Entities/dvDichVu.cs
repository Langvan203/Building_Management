using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{ 
    public class dvDichVu : BaseEntity
    {
        [Key]
        public int MaDV { get; set; }
        public string TenDV { get; set; }

        //FK
        public int MaLDV { get; set; }

        //Navigation
        public dvLoaiDV dvLoaiDV { get; set; }
        public ICollection<dvDichVuSuDung> dvDichVuSuDungs { get; set; }
    }
}
