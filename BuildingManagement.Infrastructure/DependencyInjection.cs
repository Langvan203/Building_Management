using BuildingManagement.Application.Interfaces.Repositories;
using BuildingManagement.Infrastructure.Data;
using BuildingManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingManagement.Infrastructure
{
    public static class DependencyInjection
    {
        //public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        //{
        //    //services.AddDbContext<BuildingManagementDbContext>(options =>
        //    //{
        //    //    options.UseSqlServer(
        //    //        configuration.GetConnectionString("DefaultConnection"),
        //    //        b => b.MigrationsAssembly(typeof(BuildingManagementDbContext).Assembly.FullName));
        //    //});
        //    services.AddScoped<IUnitOfWork, UnitOfWork>();

        //    return services;
        //}
    }
}
