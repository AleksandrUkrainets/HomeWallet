using AutoMapper;
using HomeWallet.Domain.Enteties;
using HomeWallet.Infrastructure.Interfaces;
using HomeWallet.Models;
using HomeWallet.Models.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWallet.Access.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly HomeWalletDbContext _homeWalletDbContext;
        private readonly IMapper _mapper;

        public OperationRepository(HomeWalletDbContext homeWalletDbContext, IMapper mapper)
        {
            _homeWalletDbContext = homeWalletDbContext;
            _mapper = mapper;
        }

        public async Task CreateOperationAsync(OperationDTO operationDto)
        {
            var operation = _mapper.Map<Operation>(operationDto);
            await _homeWalletDbContext.Operations.AddAsync(operation);
            await _homeWalletDbContext.SaveChangesAsync();
        }

        public async Task DeleteOperationAsync(int operationId)
        {
            var operationDomain = await _homeWalletDbContext.Operations.FindAsync(operationId);

            _homeWalletDbContext.Operations.Remove(operationDomain);
            await _homeWalletDbContext.SaveChangesAsync();
        }

        public async Task<OperationDTO> GetOperationAsync(int operationId)
        {
            var operationDomain = await _homeWalletDbContext.Operations.Include(x => x.Category).SingleOrDefaultAsync(c => c.Id == operationId);

            return _mapper.Map<OperationDTO>(operationDomain);
        }

        public async Task<PagedList<OperationDTO>> GetOperationsAsync(PageParameters pageParameters)
        {
            var operationDomain = await _homeWalletDbContext.Operations.OrderBy(x => x.Id).Include(x => x.Category).ToListAsync();

            return PagedList<OperationDTO>.ToPagedList(_mapper.Map<IEnumerable<OperationDTO>>(operationDomain), pageParameters.PageNumber, pageParameters.PageSize);
        }

        public async Task UpdateOperationAsync(OperationDTO operationDto)
        {
            var operationDomain = await _homeWalletDbContext.Operations.FirstOrDefaultAsync(u => u.Id == operationDto.Id);
            var operation = _mapper.Map<Operation>(operationDto);

            _homeWalletDbContext.Entry(operationDomain).CurrentValues.SetValues(operation);
            await _homeWalletDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<OperationDTO>> GetAllOperationOnDateAsync(DateTime date)
        {
            var operationsDomain = await GetAllOperationOnDateRangeAsync(date, date.AddDays(1));

            return _mapper.Map<IEnumerable<OperationDTO>>(operationsDomain);
        }

        public async Task<IEnumerable<OperationDTO>> GetAllOperationOnDateRangeAsync(DateTime sinceDate, DateTime tillDate)
        {
            var operationsDomain = await _homeWalletDbContext.Operations.Where(x => x.Date >= sinceDate && x.Date < tillDate).Include(c => c.Category).OrderBy(x => x.Id).ToListAsync();

            return _mapper.Map<IEnumerable<OperationDTO>>(operationsDomain);
        }
    }
}