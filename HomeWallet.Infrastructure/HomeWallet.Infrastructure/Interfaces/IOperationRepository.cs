using HomeWallet.Models;
using HomeWallet.Models.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWallet.Infrastructure.Interfaces
{
    public interface IOperationRepository
    {
        public Task DeleteOperationAsync(int operationId);

        public Task CreateOperationAsync(OperationDTO operation);

        public Task UpdateOperationAsync(OperationDTO operation);

        public Task<IEnumerable<OperationDTO>> GetAllOperationOnDateAsync(DateTime date);

        public Task<IEnumerable<OperationDTO>> GetAllOperationOnDateRangeAsync(DateTime sinceDate, DateTime tillDate);

        public Task<PagedList<OperationDTO>> GetOperationsAsync(PageParameters pageParameters);

        public Task<OperationDTO> GetOperationAsync(int operationId);
    }
}