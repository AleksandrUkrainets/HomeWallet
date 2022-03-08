using HomeWallet.PwaApp.HttpRepository;
using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.Pages
{
    public partial class DayBalance
    {
        private DayBalanceModel DayBalanceModel { get; set; } = new() { Date = DateTime.Now.Date, BalanceTable = new BalanceTableModel() { TotalAmount = new(), Operations = new() } };

        [Inject]
        public IBalanceHttpRepository BalanceRepository { get; set; }

        private async Task GetReport()
        {
            var day = DayBalanceModel.Date;

            var dayBalance = await BalanceRepository.GetDailyBalance(day);
            DayBalanceModel.BalanceTable.TotalAmount.ExpenseAmount = dayBalance.SumExpens;
            DayBalanceModel.BalanceTable.TotalAmount.IncomeAmount = dayBalance.SumIncome;
            DayBalanceModel.BalanceTable.Operations = dayBalance.Operations;
            if (DayBalanceModel.BalanceTable.Operations.Count == 0) DayBalanceModel.BalanceTable.Message = "There are not data.";
        }
    }
}