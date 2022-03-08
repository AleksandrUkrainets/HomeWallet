using HomeWallet.Domain.Enteties;
using HomeWallet.Infrastructure.Interfaces;
using HomeWallet.Models;
using HomeWallet.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWallet.Application
{
    public class BalanceService
    {
        private readonly IOperationRepository _operationRepository;

        public BalanceService(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        private async Task<List<OperationDTO>> GetOperationsOnDate(DateTime date)
        {
            var shortDate = date.Date;
            var operations = await _operationRepository.GetAllOperationOnDateAsync(shortDate);
            var operationsOnDate = operations.ToList();

            return operationsOnDate;
        }

        public async Task<BalanceOutput> GetDailyBalance(DateTime date)
        {
            var operationsOnDate = await GetOperationsOnDate(date);
            var incomeOperations = operationsOnDate.Where(x => x.Category.CategoryType == CategoryType.Income);
            var expensOperations = operationsOnDate.Where(x => x.Category.CategoryType == CategoryType.Expens);
            var sumIncomeOnDay = incomeOperations.Any() ? incomeOperations.Select(x => x.Amount).Aggregate((x, y) => x + y) : 0;
            var sumExpensOnDay = expensOperations.Any() ? expensOperations.Select(x => x.Amount).Aggregate((x, y) => x + y) : 0;

            var resultOndate = new BalanceOutput
            {
                SumIncomeOnDate = $"всего приход за дату \"{date}\": {sumIncomeOnDay}грн.",
                SumExpensOnDate = $"всего расход за дату \"{date}\": {sumExpensOnDay}грн.",
                SumIncome = sumIncomeOnDay,
                SumExpens = sumExpensOnDay,
                Operations = operationsOnDate
            };

            return resultOndate;
        }

        private async Task<List<OperationDTO>> GetOperationsOnDateRange(DateTime sinceDate, DateTime tillDate)
        {
            var shortSinceDate = sinceDate.Date;
            var shortTillDate = tillDate.Date;
            var operations = await _operationRepository.GetAllOperationOnDateRangeAsync(shortSinceDate, shortTillDate);
            var operationsOnRange = operations.ToList();

            return operationsOnRange;
        }

        public async Task<BalanceOutput> GetRangeBalance(DateTime sinceDate, DateTime tillDate)
        {
            var operationsOnRange = await GetOperationsOnDateRange(sinceDate, tillDate);
            var incomeOperations = operationsOnRange.Where(x => x.Category.CategoryType == CategoryType.Income);
            var expensOperations = operationsOnRange.Where(x => x.Category.CategoryType == CategoryType.Expens);
            var sumIncomeOnRange = incomeOperations.Any() ? incomeOperations.Select(x => x.Amount).Aggregate((x, y) => x + y) : 0;
            var sumExpensOnRange = expensOperations.Any() ? expensOperations.Select(x => x.Amount).Aggregate((x, y) => x + y) : 0;

            var resultOndate = new BalanceOutput
            {
                SumIncomeOnDate = $"всего приход за период с \"{sinceDate}\" до \"{tillDate}\": {sumIncomeOnRange}грн.",
                SumExpensOnDate = $"всего расход за период с \"{sinceDate}\" до \"{tillDate}\": {sumExpensOnRange}грн.",
                SumIncome = sumIncomeOnRange,
                SumExpens = sumExpensOnRange,
                Operations = operationsOnRange
            };

            return resultOndate;
        }
    }
}