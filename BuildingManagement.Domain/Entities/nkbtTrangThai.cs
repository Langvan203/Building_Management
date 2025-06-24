using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class nkbtTrangThai : BaseEntity
    {
        [Key]
        public int MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }

        //Navigation
        public ICollection<nkbtKeHoachBaoTri> nkbtKeHoachBaoTris { get; set; }
        public ICollection<nkbtChiTietBaoTri> nkbtChiTietBaoTris { get; set; }
    }
}
