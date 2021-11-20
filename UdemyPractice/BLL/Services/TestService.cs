using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions;
using DAL.DBContext;
using DAL.Model;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface ITestService
    {
        Task InsertData();
        Task DummyData1();
        Task DummyData2();
    }

    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public TestService(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task InsertData()
        {
            var dept = new Department()
            {
                Code = "arts",
                Name = "art department"
            };

            var stud = new Student()
            {
                Email = "art@gmail.com",
                Name = "mr arts"
            };

            await _unitOfWork.DepartmentRepository.CreateAsync(dept);
            await _unitOfWork.StudentRepository.CreateAsync(stud);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DummyData1()
        {
            var studentDummy = new Faker<Student>()
                //Basic rules using built-in generators
                .RuleFor(u => u.Name, (f, u) => f.Name.FullName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name));

            var departmentDummy = new Faker<Department>()
                .RuleFor(o => o.Name, f => f.Name.FirstName())
                .RuleFor(o => o.Code, f => f.Name.LastName())
                .RuleFor(u=>u.Students , f=>  studentDummy.Generate(50).ToList());

            var departmentListWithStudents = departmentDummy.Generate(100).ToList();
            
            await _context.Departments.AddRangeAsync(departmentListWithStudents);
            await _context.SaveChangesAsync();
        }

        public async Task DummyData2()
        {
            var courseDummy = new Faker<Course>()
                .RuleFor(o => o.Name, f => f.Name.FirstName())
                .RuleFor(o => o.Code, f => f.Name.LastName())
                .RuleFor(u => u.Credit, f => f.Random.Number(1, 10));

            var courseListWithStudents = courseDummy.Generate(50).ToList();

            await _context.Courses.AddRangeAsync(courseListWithStudents);
            await _unitOfWork.SaveChangesAsync();
            
            var studentIds = await _context.Students.Select(s => s.Id).ToListAsync();
            var allCourseIds = await _context.Courses.Select(c => c.Id).ToListAsync();

            var count = 0;
            foreach (var courseId in allCourseIds)
            {
                var courseStudents = new List<CourseStudent>();
                var studentIdsSkipped = studentIds.Skip(count).Take(5);

                foreach (var studentId in studentIdsSkipped)
                {
                    courseStudents.Add(new CourseStudent()
                    {
                        CourseId = courseId,
                        StudentId = studentId
                    });
                }

                await _context.CourseStudents.AddRangeAsync(courseStudents);
                await _context.SaveChangesAsync();

                count += 5;
            }
        }
    }
}
