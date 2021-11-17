using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBContext;
using DAL.Model;
using DAL.ResponseViewModel;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StudentCourseViewModel> GetSpecificStudentCourseListAsync(int studentId)
        {
            return await _context.Students.Include(s => s.CourseStudents)
                .ThenInclude(cs => cs.Course).Select(x => new StudentCourseViewModel()
                {
                    StudentId = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Courses = x.CourseStudents.Select(st => st.Course).ToList()
                }).FirstOrDefaultAsync(x => x.StudentId == studentId);
        }
    }
}
