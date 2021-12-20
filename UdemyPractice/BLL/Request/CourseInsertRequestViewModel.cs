using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Request
{
    public class CourseInsertRequestViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Credit { get; set; }
        public IFormFile CourseImage { get; set; }
    }

    public class CourseInsertRequestViewModelValidator : AbstractValidator<CourseInsertRequestViewModel>
    {
        private readonly IServiceProvider _serviceProvider;

        public CourseInsertRequestViewModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(4)
                .MaximumLength(25).MustAsync(NameExistEx).WithMessage("Name already in our database");
            RuleFor(x => x.Code).NotNull().NotEmpty().MinimumLength(3)
                .MaximumLength(10).MustAsync(CodeExistEx).WithMessage("Code already in our database");
            RuleFor(x => x.Credit).NotEmpty().NotNull();
        }

        private async Task<bool> CodeExistEx(string code, CancellationToken arg2)
        {
            if (String.IsNullOrEmpty(code))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<ICourseService>();
            var isExists = await requiredService.IsCodeExists(code);
            return isExists;
        }

        private async Task<bool> NameExistEx(string name, CancellationToken arg2)
        {
            if (String.IsNullOrEmpty(name))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<ICourseService>();
            var isExists = await requiredService.IsNameExists(name);
            return isExists;
        }
    }
}
