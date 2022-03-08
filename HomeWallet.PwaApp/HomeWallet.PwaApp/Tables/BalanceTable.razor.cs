using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;

namespace HomeWallet.PwaApp.Tables
{
    public partial class BalanceTable
    {
        [Parameter]
        public BalanceTableModel BalanceTableModel { get; set; }

        private string _incomeColor = "#8dc1aa";
        private string _expenseColor = "#FF4500";
    }
}