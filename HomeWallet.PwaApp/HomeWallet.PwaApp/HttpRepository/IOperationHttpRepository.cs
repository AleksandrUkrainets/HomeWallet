using HomeWallet.Models.RequestFeatures;
using HomeWallet.PwaApp.Features;
using HomeWallet.PwaApp.Models;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.HttpRepository
{
    public interface IOperationHttpRepository
    {
        public Task DeleteOperation(string id);

        public Task<bool> CreateOperation(OperationPwa category);

        public Task<bool> UpdateOperation(OperationPwa category);

        public Task<OperationPwa> GetOperation(string id);

        public Task<PagingResponse<OperationPwa>> GetOperations(PageParameters pageParameters);
    }
}