using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class tnPhongBan : BaseEntity
    {
        [Key]
        public int MaPB { get; set; }
        public string TenPB { get; set; }
        public int? MaTN { get; set; }
        public ICollection<tnNhanVien> tnNhanViens { get; set; }
        public tnToaNha tnToaNha { get; set; }
    }
}
