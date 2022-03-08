using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.Tables
{
    public partial class CategoryTable
    {
        [Parameter]
        public List<CategoryPwa> Categories { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private async Task Delete(int id)
        {
            var category = Categories.FirstOrDefault(p => p.Id.Equals(id));

            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {category.Name} category?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
            }
        }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updateCategory/", id.ToString());
            NavigationManager.NavigateTo(url);
        }
    }
}