using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.Tables
{
    public partial class OperationTable
    {
        [Parameter]
        public List<OperationPwa> Operations { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private async Task Delete(int id)
        {
            var operation = Operations.FirstOrDefault(p => p.Id.Equals(id));

            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {operation.Category.Name} =  {operation.Amount} operation?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
            }
        }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updateOperation/", id.ToString());
            NavigationManager.NavigateTo(url);
        }
    }
}