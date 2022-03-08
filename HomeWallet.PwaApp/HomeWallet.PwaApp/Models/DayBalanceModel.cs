using System;

namespace HomeWallet.PwaApp.Models
{
    public class DayBalanceModel
    {
        public DateTime Date { get; set; }
        public BalanceTableModel BalanceTable { get; set; }
    }
}