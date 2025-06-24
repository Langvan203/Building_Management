using BuildingManagement.Application.DTOs.Request;
using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Domain.Entities;
using BuildingManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(BuildingManagementDbContext context) : base(context)
        {

        }

        public async Task<List<RoleDTO>> GetDSRole()
        {
            var dsRole = await _context.Roles.Select(x => new RoleDTO
            {
                RoleID = x.RoleID,
                RoleName = x.RoleName,
            }).ToListAsync();
            return dsRole;
        }
    }

}
