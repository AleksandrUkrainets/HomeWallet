using System.Collections.Generic;

namespace HomeWallet.PwaApp.Models
{
    public class BalanceTableModel
    {
        public TotalAmountModel TotalAmount { get; set; }
        public List<OperationPwa> Operations { get; set; }
        public string Message { get; set; } = "Enter the date(s) and click GetReport button.";
    }
}