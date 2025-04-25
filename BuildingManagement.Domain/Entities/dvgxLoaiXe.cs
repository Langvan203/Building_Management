using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class dvgxLoaiXe : BaseEntity
    {
        [Key]
        public int MaLX { get; set; }
        public string TenLX { get; set; }
        public decimal DonGia { get; set; }


        // Navigation
        public ICollection<dvgxTheXe> dvgxTheXes { get; set; }
    }
}
