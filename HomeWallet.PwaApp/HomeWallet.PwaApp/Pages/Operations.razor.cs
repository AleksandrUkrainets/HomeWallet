using HomeWallet.Models.RequestFeatures;
using HomeWallet.PwaApp.HttpRepository;
using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.Pages
{
    public partial class Operations
    {
        private List<OperationPwa> OperationsPwa { get; set; } = new();

        public MetaData MetaData { get; set; } = new MetaData();

        private PageParameters _pageParameters = new();

        [Inject]
        public IOperationHttpRepository OperationHttpRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetOperations();
            await base.OnInitializedAsync();
        }

        private async Task SelectedPage(int page)
        {
            _pageParameters.PageNumber = page;
            await GetOperations();
        }

        private async Task GetOperations()
        {
            var pagingResponse = await OperationHttpRepository.GetOperations(_pageParameters);
            OperationsPwa = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task DeleteOperation(int id)
        {
            await OperationHttpRepository.DeleteOperation(id.ToString());
            await OnInitializedAsync();
        }
    }
}