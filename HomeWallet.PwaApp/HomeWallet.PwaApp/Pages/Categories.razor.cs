using HomeWallet.Models.RequestFeatures;
using HomeWallet.PwaApp.HttpRepository;
using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.Pages
{
    public partial class Categories
    {
        private List<CategoryPwa> CategoriesPwa { get; set; } = new();

        public MetaData MetaData { get; set; } = new MetaData();

        private PageParameters _pageParameters = new();

        [Inject]
        public ICategoryHttpRepository CategoryHttpRepository { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCategories();
            await base.OnInitializedAsync();
        }

        private async Task SelectedPage(int page)
        {
            _pageParameters.PageNumber = page;
            await GetCategories();
        }

        private async Task GetCategories()
        {
            var pagingResponse = await CategoryHttpRepository.GetCategories(_pageParameters);
            CategoriesPwa = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task DeleteCategory(int id)
        {
            try
            {
                await CategoryHttpRepository.DeleteCategory(id.ToString());
            }
            catch
            {
                await Js.InvokeVoidAsync("alert", "Error of delete. There are relations with Operations!");
            }
            await OnInitializedAsync();
        }
    }
}