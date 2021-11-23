using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.DBContext;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomerBalanceRepository : RepositoryBase<CustomerBalance>, ICustomerBalanceRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerBalanceRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task MustUpdateBalanceAsync(string email, decimal amount)
        {
            var customerBalance = await _context.CustomerBalances.FirstOrDefaultAsync(x => x.Email == email);
            customerBalance.Balance += amount;

            var isUpdated = false;

            do
            {
                try
                {
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        isUpdated = true;
                    }
                }
                catch (DbUpdateConcurrencyException e)
                {
                    foreach (var entry in e.Entries)
                    {
                        if(!(entry.Entity is CustomerBalance)) continue;

                        var databaseEntry = entry.GetDatabaseValues();
                        var databaseValues = (CustomerBalance)databaseEntry.ToObject();
                        databaseValues.Balance += amount;

                        entry.OriginalValues.SetValues(databaseEntry);
                        entry.CurrentValues.SetValues(databaseValues);
                    }
                }
            } while (isUpdated);
        }
    }
}