using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.DBContext;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context): base(context)
        {
            
        }
    }
}