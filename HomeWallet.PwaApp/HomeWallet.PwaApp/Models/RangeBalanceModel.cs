using System;

namespace HomeWallet.PwaApp.Models
{
    public class RangeBalanceModel
    {
        public DateTime SinceDate { get; set; }
        public DateTime TillDate { get; set; }
        public BalanceTableModel BalanceTable { get; set; }
    }
}