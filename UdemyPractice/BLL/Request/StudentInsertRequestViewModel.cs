using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Request
{
    public class StudentInsertRequestViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
    }

    public class StudentInsertRequestViewModelValidator : AbstractValidator<StudentInsertRequestViewModel>
    {
        private readonly IServiceProvider _serviceProvider;

        public StudentInsertRequestViewModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(4)
                .MaximumLength(25);
            RuleFor(x => x.Email).NotNull().NotEmpty().MinimumLength(3).EmailAddress()
                .MaximumLength(10).MustAsync(EmailExistEx).WithMessage("Email already in our database");
            RuleFor(x => x.DepartmentId).GreaterThan(0)
                .MustAsync(DepartmentExistEx).WithMessage("Department not exist in our database");
        }

        private async Task<bool> EmailExistEx(string email, CancellationToken arg2)
        {
            if (String.IsNullOrEmpty(email))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IStudentService>();
            var isExists = await requiredService.IsEmailExists(email);
            return isExists;
        }

        private async Task<bool> DepartmentExistEx(int id, CancellationToken arg2)
        {
            if (id == 0)
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            var isExists = !await requiredService.IsIdExists(id);
            return isExists;
        }
    }
}
