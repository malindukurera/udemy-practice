using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.DBContext;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context): base(context)
        {
            
        }
    }
}