using System;
using System.Collections.Generic;
using System.Text;
using BLL.Request;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL;
using FluentValidation;
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
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ICourseStudentService, CourseStudentService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<ITransactionService, TransactionService>();

            AllFluentValidationDependancy(services);
        }

        private static void AllFluentValidationDependancy(IServiceCollection services)
        {
            services.AddTransient<IValidator<DepartmentInsertRequestViewModel>, DepartmentInsertRequestViewModelValidator>();
            services.AddTransient<IValidator<StudentInsertRequestViewModel>, StudentInsertRequestViewModelValidator>();
            services.AddTransient<IValidator<CourseInsertRequestViewModel>, CourseInsertRequestViewModelValidator>();
            services.AddTransient<IValidator<CourseStudentInsertRequestViewModel>, CourseStudentInsertRequestViewModelValidator>();
        }
    }
}
