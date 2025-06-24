using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class dvLoaiDV : BaseEntity
    {
        [Key]
        public int MaLDV { get; set; }
        public string TenLDV { get; set; }
        public string? Icon { get; set; }
        public string? MoTa { get; set; }
        public bool IsEssential { get; set; } = false;

        //FK
        public int? MaTN { get; set; }

        //Navigation
        public ICollection<dvDichVu> dvDichVus { get; set; }
        public tnToaNha tnToaNha { get; set; }
    }
}
