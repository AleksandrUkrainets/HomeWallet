using HomeWallet.Models.RequestFeatures;
using HomeWallet.PwaApp.HttpRepository;
using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.Pages
{
    public partial class CreateOperation
    {
        private readonly OperationPwa _operation = new() { Date = DateTime.Now.Date, Category = new() };

        private List<CategoryPwa> _categories = new();

        [Inject]
        public IOperationHttpRepository OperationHttpRepository { get; set; }

        [Inject]
        public ICategoryHttpRepository CategoryHttpRepository { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        private void Back()
        {
            Navigation.NavigateTo($"/operations");
        }

        protected override async Task OnInitializedAsync()
        {
            var pagingResponse = await CategoryHttpRepository.GetCategories(new PageParameters());
            _categories = pagingResponse.Items;
            await base.OnInitializedAsync();
        }

        private async Task CreateAsync()
        {
            var selectedCategory = _categories.First(category => category.Id == _operation.CategoryId);
            _operation.Category = selectedCategory;
            _operation.CategoryId = _operation.CategoryId;

            if (await OperationHttpRepository.CreateOperation(_operation))
            {
                Navigation.NavigateTo($"/operations");
            }
            else
            {
                await Js.InvokeVoidAsync("alert", "Operation with same name already exist.");
                Navigation.NavigateTo($"/createOperation");
            }
        }
    }
}