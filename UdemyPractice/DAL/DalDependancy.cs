using System;
using System.Collections.Generic;
using System.Text;
using DAL.DBContext;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DalDependancy
    {
        public static void AllDependancy(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            
            // repository dependancy

            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
        }
    }
}
