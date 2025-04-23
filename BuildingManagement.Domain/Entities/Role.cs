using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class Role : BaseEntity
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public ICollection<tnNhanVien> tnNhanViens { get; set; }
    }
}
