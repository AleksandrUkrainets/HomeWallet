using HomeWallet.PwaApp.Models;
using System;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.HttpRepository
{
    public interface IBalanceHttpRepository
    {
        public Task<BalancePwa> GetDailyBalance(DateTime date);

        public Task<BalancePwa> GetRangeBalance(DateTime sinceDate, DateTime tillDate);
    }
}