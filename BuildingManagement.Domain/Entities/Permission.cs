using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }

        public ICollection<Role> Roles { get; set; }

    }
}
