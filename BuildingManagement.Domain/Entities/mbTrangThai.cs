using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class mbTrangThai : BaseEntity
    {
        [Key]
        public int MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }

        // Navigation
        public ICollection<tnMatBang> tnMatBangs { get; set; }
    }
}
