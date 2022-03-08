using HomeWallet.Models;
using HomeWallet.Models.RequestFeatures;
using System.Threading.Tasks;

namespace HomeWallet.Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<PagedList<CategoryDTO>> GetCategoriesAsync(PageParameters pageParameters);

        public Task<CategoryDTO> GetCategoryAsync(int id);

        public Task DeleteCategoryAsync(int categoryId);

        public Task CreateCategoryAsync(CategoryDTO category);

        public Task UpdateCategoryAsync(CategoryDTO category);

        public Task<int> GetLastCategoryId();
    }
}