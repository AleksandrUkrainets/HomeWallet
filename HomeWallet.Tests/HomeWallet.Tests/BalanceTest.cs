using HomeWallet.Application;
using HomeWallet.Infrastructure.Interfaces;
using HomeWallet.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace HomeWallet.Tests
{
    public class BalanceTest
    {
        private readonly BalanceService _balanceService;
        private readonly Mock<IOperationRepository> _operationRepository;

        public BalanceTest()
        {
            _operationRepository = new Mock<IOperationRepository>();
            _balanceService = new BalanceService(_operationRepository.Object);
        }

        [Fact]
        public void GetValidDayBalance_CallsGetDailyBalance()
        {
            var dateNow = DateTime.Now;
            decimal firstOperationAmount = 70;
            decimal secondOperationAmount = 30;
            CategoryDTO mockFirstIncomeCategory = new CategoryDTO { CategoryType = Domain.Enteties.CategoryType.Income, Id = 1, Name = "firstIncomeCategory" };
            CategoryDTO mockSecondIncomeCategory = new CategoryDTO { CategoryType = Domain.Enteties.CategoryType.Income, Id = 2, Name = "secondIncomeCategory" };

            IEnumerable<OperationDTO> mockOperationsOnDate = new List<OperationDTO>()
            {
                new OperationDTO { Id = 1, CategoryId = 1, Category = mockFirstIncomeCategory, Amount = firstOperationAmount, Date = dateNow },
                new OperationDTO { Id = 2, CategoryId = 2, Category = mockSecondIncomeCategory, Amount = secondOperationAmount, Date = dateNow}
            };

            decimal expectedSumIncomeOnDay = firstOperationAmount + secondOperationAmount;
            decimal expectedSumExpensOnDay = 0;

            _operationRepository.Setup(x => x.GetAllOperationOnDateAsync(dateNow.Date)).ReturnsAsync(mockOperationsOnDate);

            var actualDayBalance = _balanceService.GetDailyBalance(dateNow).Result;

            Assert.Equal(expectedSumIncomeOnDay, actualDayBalance.SumIncome);
            Assert.Equal(expectedSumExpensOnDay, actualDayBalance.SumExpens);
        }

        [Fact]
        public void GetInvalidDayBalance_CallsGetDailyBalance()
        {
            var dateNow = DateTime.Now;
            decimal firstOperationAmount = 70;
            decimal secondOperationAmount = 30;
            CategoryDTO mockFirstIncomeCategory = new CategoryDTO { CategoryType = Domain.Enteties.CategoryType.Income, Id = 1, Name = "firstIncomeCategory" };
            CategoryDTO mockFirstExpensCategory = new CategoryDTO { CategoryType = Domain.Enteties.CategoryType.Expens, Id = 2, Name = "firstExpensCategory" };

            IEnumerable<OperationDTO> mockOperationsOnDate = new List<OperationDTO>()
            {
                new OperationDTO { Id = 1, CategoryId = 1, Category = mockFirstIncomeCategory, Amount = firstOperationAmount, Date = dateNow },
                new OperationDTO { Id = 2, CategoryId = 2, Category = mockFirstExpensCategory, Amount = secondOperationAmount, Date = dateNow}
            };

            decimal expectedSumIncomeOnDay = firstOperationAmount + secondOperationAmount;
            decimal expectedSumExpensOnDay = 0;

            _operationRepository.Setup(x => x.GetAllOperationOnDateAsync(dateNow.Date)).ReturnsAsync(mockOperationsOnDate);

            var actualDayBalance = _balanceService.GetDailyBalance(dateNow).Result;

            Assert.NotEqual(expectedSumIncomeOnDay, actualDayBalance.SumIncome);
            Assert.NotEqual(expectedSumExpensOnDay, actualDayBalance.SumExpens);
        }

        [Fact]
        public void GetValidRangeBalance_CallsGetRangeBalance()
        {
            var sinceDate = DateTime.Now;
            var tillDate = DateTime.Now.AddDays(1);
            decimal firstOperationAmount = 70;
            decimal secondOperationAmount = 30;
            CategoryDTO mockFirstIncomeCategory = new CategoryDTO { CategoryType = Domain.Enteties.CategoryType.Income, Id = 1, Name = "firstIncomeCategory" };
            CategoryDTO mockSecondIncomeCategory = new CategoryDTO { CategoryType = Domain.Enteties.CategoryType.Income, Id = 2, Name = "secondIncomeCategory" };

            IEnumerable<OperationDTO> mockOperationsOnRange = new List<OperationDTO>()
            {
                new OperationDTO { Id = 1, CategoryId = 1, Category = mockFirstIncomeCategory, Amount = firstOperationAmount, Date = sinceDate },
                new OperationDTO { Id = 2, CategoryId = 2, Category = mockSecondIncomeCategory, Amount = secondOperationAmount, Date = tillDate}
            };

            decimal expectedSumIncomeOnRange = firstOperationAmount + secondOperationAmount;
            decimal expectedSumExpensOnRange = 0;

            _operationRepository.Setup(x => x.GetAllOperationOnDateRangeAsync(sinceDate.Date, tillDate.Date)).ReturnsAsync(mockOperationsOnRange);

            var actualRangeBalance = _balanceService.GetRangeBalance(sinceDate, tillDate).Result;

            Assert.Equal(expectedSumIncomeOnRange, actualRangeBalance.SumIncome);
            Assert.Equal(expectedSumExpensOnRange, actualRangeBalance.SumExpens);
        }

        [Fact]
        public void GetInvalidRangeBalance_CallsGetRangeBalance()
        {
            var sinceDate = DateTime.Now;
            var tillDate = DateTime.Now.AddDays(3);
            var firstOperationDate = DateTime.Now.AddDays(1);
            var secondOperationDate = DateTime.Now.AddDays(2);
            decimal firstOperationAmount = 70;
            decimal secondOperationAmount = 30;
            CategoryDTO mockFirstIncomeCategory = new CategoryDTO { CategoryType = Domain.Enteties.CategoryType.Income, Id = 1, Name = "firstIncomeCategory" };
            CategoryDTO mockFirstExpensCategory = new CategoryDTO { CategoryType = Domain.Enteties.CategoryType.Expens, Id = 2, Name = "firstExpensCategory" };

            IEnumerable<OperationDTO> mockOperationsOnRange = new List<OperationDTO>()
            {
                new OperationDTO { Id = 1, CategoryId = 1, Category = mockFirstIncomeCategory, Amount = firstOperationAmount, Date = firstOperationDate },
                new OperationDTO { Id = 2, CategoryId = 2, Category = mockFirstExpensCategory, Amount = secondOperationAmount, Date = secondOperationDate}
            };

            decimal expectedSumIncomeOnRange = firstOperationAmount + secondOperationAmount;
            decimal expectedSumExpensOnRange = 0;

            _operationRepository.Setup(x => x.GetAllOperationOnDateRangeAsync(sinceDate.Date, tillDate.Date)).ReturnsAsync(mockOperationsOnRange);

            var actualRangeBalance = _balanceService.GetRangeBalance(sinceDate, tillDate).Result;

            Assert.NotEqual(expectedSumIncomeOnRange, actualRangeBalance.SumIncome);
            Assert.NotEqual(expectedSumExpensOnRange, actualRangeBalance.SumExpens);
        }
    }
}