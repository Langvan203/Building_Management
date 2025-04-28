using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Infrastructure.Data.Context
{
    public class BuildingManagementDbContextFactory : IDesignTimeDbContextFactory<BuildingManagementDbContext>
    {
        public BuildingManagementDbContext CreateDbContext(string[] args) 
        {
            var optionsBuilder = new DbContextOptionsBuilder<BuildingManagementDbContext>();
            optionsBuilder.UseSqlServer("Data Source=NITRO5;Initial Catalog=BuildingManagement;Integrated Security=True;Trust Server Certificate=True");
            return new BuildingManagementDbContext(optionsBuilder.Options);
        }

        
    }
}
