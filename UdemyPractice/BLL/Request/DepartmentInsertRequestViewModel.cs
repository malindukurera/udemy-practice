using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL.Services;
using DAL.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Request
{
    public class DepartmentInsertRequestViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class DepartmentInsertRequestViewModelValidator : AbstractValidator<DepartmentInsertRequestViewModel>
    {
        private readonly IServiceProvider _serviceProvider;

        public DepartmentInsertRequestViewModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(4)
                .MaximumLength(25).MustAsync(NameExistEx).WithMessage("Name already in our database");
            RuleFor(x => x.Code).NotNull().NotEmpty().MinimumLength(3)
                .MaximumLength(10).MustAsync(CodeExistEx).WithMessage("Code already in our database");
        }

        private async Task<bool> CodeExistEx(string code, CancellationToken arg2)
        {
            if (String.IsNullOrEmpty(code))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            var isExists = await requiredService.IsCodeExists(code);
            return isExists;
        }

        private async Task<bool> NameExistEx(string name, CancellationToken arg2)
        {
            if (String.IsNullOrEmpty(name))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            var isExists = await requiredService.IsNameExists(name);
            return isExists;
        }
    }
}
