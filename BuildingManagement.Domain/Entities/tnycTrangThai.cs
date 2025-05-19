using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class tnycTrangThai : BaseEntity
    {
        [Key]
        public int IdTrangThai { get; set; }
        public string TenTrangThai { get; set; }

        // Navigation
        public ICollection<tnycYeuCauSuaChua> tnycYeuCaus { get; set; }
    }
}
