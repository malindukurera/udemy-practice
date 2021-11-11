using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL.Services;
using BLL.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Request
{
    public class CourseStudentInsertRequestViewModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }

    public class CourseStudentInsertRequestViewModelValidator : AbstractValidator<CourseStudentInsertRequestViewModel>
    {
        private readonly IServiceProvider _serviceProvider;

        public CourseStudentInsertRequestViewModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            RuleFor(x => x.StudentId).NotNull().NotEmpty()
                .MustAsync(StudentIdExistEx).WithMessage("Student not exist database");
            RuleFor(x => x.CourseId).NotNull().NotEmpty()
                .MustAsync(CourseIdExistEx).WithMessage("Course not exist database");
        }

        private async Task<bool> StudentIdExistEx(int id, CancellationToken arg2)
        {
            var requiredService = _serviceProvider.GetRequiredService<ICourseService>();
            var isExists = await requiredService.IsIdExists(id);
            return isExists;
        }

        private async Task<bool> CourseIdExistEx(int id, CancellationToken arg2)
        {
            var requiredService = _serviceProvider.GetRequiredService<IStudentService>();
            var isExists = await requiredService.IsIdExists(id);
            return isExists;
        }
    }
}
