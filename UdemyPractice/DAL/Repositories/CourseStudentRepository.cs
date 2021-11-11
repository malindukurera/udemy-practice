using System;
using System.Collections.Generic;
using System.Text;
using DAL.DBContext;
using DAL.Model;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class CourseStudentRepository : RepositoryBase<CourseStudent>, ICourseStudentRepository
    {
        public CourseStudentRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
