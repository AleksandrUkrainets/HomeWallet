using System.Collections.Generic;

namespace HomeWallet.Models.ApiModels
{
    public class BalanceOutput
    {
        public string SumIncomeOnDate { get; set; }

        public string SumExpensOnDate { get; set; }

        public decimal SumIncome { get; set; }

        public decimal SumExpens { get; set; }

        public List<OperationDTO> Operations { get; set; }
    }
}