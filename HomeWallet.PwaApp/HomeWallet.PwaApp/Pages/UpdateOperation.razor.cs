using HomeWallet.Models.RequestFeatures;
using HomeWallet.PwaApp.HttpRepository;
using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.Pages
{
    public partial class UpdateOperation
    {
        private OperationPwa _operation;
        private List<CategoryPwa> _categories = new();

        [Inject]
        public IOperationHttpRepository OperationHttpRepository { get; set; }

        [Inject]
        public ICategoryHttpRepository CategoryHttpRepository { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public string Id { get; set; }

        private string _selectedId;

        protected override async Task OnInitializedAsync()
        {
            _operation = await OperationHttpRepository.GetOperation(Id);
            _selectedId = _operation.Category.Id.ToString();
            var pagingResponse = await CategoryHttpRepository.GetCategories(new PageParameters());
            _categories = pagingResponse.Items;
        }

        private async Task UpdateAsync()
        {
            int selectId = int.Parse(_selectedId);
            var selectedCategory = _categories.First(category => category.Id == selectId);
            _operation.Category = selectedCategory;
            _operation.CategoryId = selectId;
            await OperationHttpRepository.UpdateOperation(_operation);
            Navigation.NavigateTo($"/operations");
        }

        private void Back()
        {
            Navigation.NavigateTo($"/operations");
        }
    }
}