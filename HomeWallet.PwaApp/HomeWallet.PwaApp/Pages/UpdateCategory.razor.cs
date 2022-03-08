using HomeWallet.PwaApp.HttpRepository;
using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.Pages
{
    public partial class UpdateCategory
    {
        private CategoryPwa _category = new();

        [Inject]
        private ICategoryHttpRepository CategoryRepository { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _category = await CategoryRepository.GetCategory(Id);
        }

        private async Task UpdateAsync()
        {
            if (await CategoryRepository.UpdateCategory(_category))
            {
                Navigation.NavigateTo($"/categories");
            }
            else
            {
                await Js.InvokeVoidAsync("alert", "Category with same name already exist.");
                Navigation.NavigateTo($"/updateCategory/{_category.Id}");
            }
        }

        private void Back()
        {
            Navigation.NavigateTo($"/categories");
        }
    }
}