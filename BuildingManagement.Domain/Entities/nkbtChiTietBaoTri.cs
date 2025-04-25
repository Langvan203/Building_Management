using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class nkbtChiTietBaoTri : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string GhiChu { get; set; }

        //FK
        public int MaKeHoach { get; set; }
        //Navigation
        public nkbtKeHoachBaoTri nkbtKeHoachBaoTri { get; set; }
    }
}
