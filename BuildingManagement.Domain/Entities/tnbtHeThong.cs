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

        //FK
        public int MaMB { get; set; }

        //Navigation
        public tnMatBang tnMatBang { get; set; }

        public ICollection<nkbtKeHoachBaoTri> nkbtKeHoachBaoTris { get; set; }
    }
}
