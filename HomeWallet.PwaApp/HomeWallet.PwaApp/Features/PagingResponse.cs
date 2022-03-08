using HomeWallet.Models.RequestFeatures;
using System.Collections.Generic;

namespace HomeWallet.PwaApp.Features
{
    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}