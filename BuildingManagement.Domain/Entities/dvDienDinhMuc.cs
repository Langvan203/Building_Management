using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class dvDienDinhMuc : BaseEntity
    {
        [Key]
        public int MaDM { get; set; }
        public string TenDM { get; set; }
        public decimal DonGiaDinhMuc { get; set; }

        // Navigation
        public ICollection<dvDien> dvDien { get; set; }
    }
}
