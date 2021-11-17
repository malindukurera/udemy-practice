using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using DAL.ResponseViewModel;

namespace DAL.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        Task<StudentCourseViewModel> GetSpecificStudentCourseListAsync(int studentId);
    }
}
