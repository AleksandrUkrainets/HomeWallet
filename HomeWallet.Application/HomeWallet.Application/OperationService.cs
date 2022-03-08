using HomeWallet.Infrastructure.Interfaces;
using HomeWallet.Models;
using HomeWallet.Models.RequestFeatures;
using System.Threading.Tasks;

namespace HomeWallet.Application
{
    public class OperationService
    {
        private readonly IOperationRepository _operationRepository;
        private readonly ICategoryRepository _categoryRepository;

        public OperationService(IOperationRepository operationRepository, ICategoryRepository categoryRepository)
        {
            _operationRepository = operationRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task CreateOperation(OperationDTO operation)
        {
            if (operation.CategoryId == 0)
            {
                await _categoryRepository.CreateCategoryAsync(operation.Category);
                var idLastCategory = _categoryRepository.GetLastCategoryId().Result;
                operation.CategoryId = idLastCategory;
            }

            operation.Amount = operation.Category.CategoryType == Domain.Enteties.CategoryType.Income ? operation.Amount : -operation.Amount;

            await _operationRepository.CreateOperationAsync(operation); ;
        }

        public async Task UpdateOperation(OperationDTO operation)
        {
            operation.Amount = operation.Category.CategoryType == Domain.Enteties.CategoryType.Income ? operation.Amount : -operation.Amount;

            await _operationRepository.UpdateOperationAsync(operation);
        }

        public async Task DeleteOperation(int id)
        {
            await _operationRepository.DeleteOperationAsync(id);
        }

        public async Task<PagedList<OperationDTO>> GetOperations(PageParameters pageParameters)
        {
            var operations = await _operationRepository.GetOperationsAsync(pageParameters);

            return operations;
        }

        public async Task<OperationDTO> GetOperation(int id)
        {
            return await _operationRepository.GetOperationAsync(id);
        }
    }
}