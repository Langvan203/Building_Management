using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class tnKhoiNha : BaseEntity
    {
        [Key]
        public int MaKN { get; set; }
        public string TenKN { get; set; }

        //FK
        public int MaTN { get; set; }
        
        //Navigation
        public tnToaNha tnToaNha { get; set; }

        public ICollection<tnTangLau> tnTangLaus { get; set; }
    }
}
