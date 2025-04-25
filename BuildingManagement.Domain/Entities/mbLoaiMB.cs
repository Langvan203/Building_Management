using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class mbLoaiMB : BaseEntity
    {
        [Key]
        public int MaLMB { get; set; }
        public string TenLMB { get; set; }

        // Navigation
        public ICollection<tnMatBang> tnMatBangs { get; set; }
    }
}
