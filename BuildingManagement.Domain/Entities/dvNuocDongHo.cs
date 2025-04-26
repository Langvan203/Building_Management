using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class dvNuocDongHo : BaseEntity
    {
        [Key]
        public int MaDH { get; set; }

        //FK 
        public int MaMB { get; set; }
        
        //Navigation
        public tnMatBang tnMatBang { get; set; }
        public ICollection<dvNuoc> dvNuocs { get; set; }
    }
}
