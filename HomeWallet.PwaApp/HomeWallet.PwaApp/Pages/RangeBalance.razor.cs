using HomeWallet.PwaApp.HttpRepository;
using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.Pages
{
    public partial class RangeBalance
    {
        private RangeBalanceModel RangeBalanceModel { get; } = new() { SinceDate = DateTime.Now, TillDate = DateTime.Now, BalanceTable = new BalanceTableModel() { TotalAmount = new(), Operations = new() } };

        [Inject]
        public IBalanceHttpRepository BalanceHttpRepository { get; set; }

        private async Task GetReport()
        {
            var sinceDate = RangeBalanceModel.SinceDate;
            var tillDate = RangeBalanceModel.TillDate;

            var rangeBalance = await BalanceHttpRepository.GetRangeBalance(sinceDate, tillDate);
            RangeBalanceModel.BalanceTable.TotalAmount.ExpenseAmount = rangeBalance.SumExpens;
            RangeBalanceModel.BalanceTable.TotalAmount.IncomeAmount = rangeBalance.SumIncome;
            RangeBalanceModel.BalanceTable.Operations = rangeBalance.Operations;
            if (RangeBalanceModel.BalanceTable.Operations.Count == 0) RangeBalanceModel.BalanceTable.Message = "There are not data.";
        }
    }
}