using HomeWallet.Models.RequestFeatures;
using HomeWallet.PwaApp.Features;
using HomeWallet.PwaApp.Models;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.HttpRepository
{
    public interface ICategoryHttpRepository
    {
        public Task<PagingResponse<CategoryPwa>> GetCategories(PageParameters pageParameters);

        public Task DeleteCategory(string id);

        public Task<bool> CreateCategory(CategoryPwa category);

        public Task<bool> UpdateCategory(CategoryPwa category);

        public Task<CategoryPwa> GetCategory(string id);
    }
}