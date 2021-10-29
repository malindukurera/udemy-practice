using System;
using System.Collections.Generic;
using System.Text;
using BLL.Services;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class BllDependancy
    {
        public static void AllDependancy(IServiceCollection services, IConfiguration configuration)
        {
            DalDependancy.AllDependancy(services, configuration);
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IStudentService, StudentService>();
        }
    }
}
