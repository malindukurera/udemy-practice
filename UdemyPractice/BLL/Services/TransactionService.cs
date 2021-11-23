using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Services.Interfaces;
using DAL.Model;
using DAL.Repositories;

namespace BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task InitialTransaction()
        {
            var rand = new Random();
            var amount = rand.Next(1000);

            var trans = new TransactionHistory();
            trans.Amount = amount;

            await _unitOfWork.TransactionHistoryRepository.CreateAsync(trans);
            if (await _unitOfWork.SaveChangesAsync())
            {
                await _unitOfWork.CustomerBalanceRepository.MustUpdateBalanceAsync("nimal@gmail.com", amount);
            }
        }
    }
}
