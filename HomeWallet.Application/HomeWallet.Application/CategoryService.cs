using HomeWallet.Infrastructure.Interfaces;
using HomeWallet.Models;
using HomeWallet.Models.RequestFeatures;
using System.Threading.Tasks;

namespace HomeWallet.Application
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedList<CategoryDTO>> GetCategories(PageParameters pageParameters)
        {
            var categories = await _categoryRepository.GetCategoriesAsync(pageParameters);

            return categories;
        }

        public async Task<CategoryDTO> GetCategory(int id)
        {
            return await _categoryRepository.GetCategoryAsync(id);
        }

        public async Task CreateCategory(CategoryDTO category)
        {
            await _categoryRepository.CreateCategoryAsync(category);
        }

        public async Task UpdateCategory(CategoryDTO category)
        {
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}