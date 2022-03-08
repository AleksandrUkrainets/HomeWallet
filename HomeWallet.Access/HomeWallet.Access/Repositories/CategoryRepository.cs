using AutoMapper;
using HomeWallet.Domain.Enteties;
using HomeWallet.Infrastructure.Interfaces;
using HomeWallet.Models;
using HomeWallet.Models.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWallet.Access.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HomeWalletDbContext _homeWalletDbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(HomeWalletDbContext homeWalletDbContext, IMapper mapper)
        {
            _homeWalletDbContext = homeWalletDbContext;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CategoryDTO categoryDto)
        {
            await _homeWalletDbContext.Categories.AddAsync(_mapper.Map<Category>(categoryDto));
            await _homeWalletDbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var categoryDomain = await _homeWalletDbContext.Categories.FindAsync(categoryId);

            _homeWalletDbContext.Categories.Remove(categoryDomain);
            await _homeWalletDbContext.SaveChangesAsync();
        }

        public async Task<PagedList<CategoryDTO>> GetCategoriesAsync(PageParameters pageParameters)
        {
            var categoriesDomain = await _homeWalletDbContext.Categories.OrderBy(x => x.Name).ToListAsync();

            return PagedList<CategoryDTO>.ToPagedList(_mapper.Map<IEnumerable<CategoryDTO>>(categoriesDomain), pageParameters.PageNumber, pageParameters.PageSize);
        }

        public async Task<CategoryDTO> GetCategoryAsync(int id)
        {
            var categoryDomain = await _homeWalletDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<CategoryDTO>(categoryDomain);
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDto)
        {
            var categoryDomain = await _homeWalletDbContext.Categories.OrderBy(x => x.Id).FirstOrDefaultAsync(u => u.Id == categoryDto.Id);

            _homeWalletDbContext.Entry(categoryDomain).CurrentValues.SetValues(_mapper.Map<Category>(categoryDto));
            await _homeWalletDbContext.SaveChangesAsync();
        }

        public async Task<int> GetLastCategoryId()
        {
            return await _homeWalletDbContext.Categories.OrderBy(x => x.Id).Select(c => c.Id).LastOrDefaultAsync();
        }
    }
}