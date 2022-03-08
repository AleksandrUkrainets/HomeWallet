using HomeWallet.PwaApp.HttpRepository;
using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.Pages
{
    public partial class CreateCategory
    {
        private readonly CategoryPwa _category = new();

        [Inject]
        public ICategoryHttpRepository CategoryHttpRepository { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        private void Back()
        {
            Navigation.NavigateTo($"/categories");
        }

        private async Task CreateAsync()
        {
            if (await CategoryHttpRepository.CreateCategory(_category))
            {
                Navigation.NavigateTo($"/categories");
            }
            else
            {
                await Js.InvokeVoidAsync("alert", "Category with same name already exist.");
                Navigation.NavigateTo($"/createCategory");
            }
        }
    }
}