using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.Repositories
{
    public interface ICustomerBalanceRepository : IRepositoryBase<CustomerBalance>
    {
        Task MustUpdateBalanceAsync(string email, decimal amount);
    }
}