using System.Collections.Generic;

namespace HomeWallet.PwaApp.Models
{
    public class BalancePwa
    {
        public decimal SumIncome { get; set; }

        public decimal SumExpens { get; set; }

        public List<OperationPwa> Operations { get; set; }
    }
}