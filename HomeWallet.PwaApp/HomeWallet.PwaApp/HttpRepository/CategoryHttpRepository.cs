using HomeWallet.Models.RequestFeatures;
using HomeWallet.PwaApp.Features;
using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.HttpRepository
{
    public class CategoryHttpRepository : ICategoryHttpRepository
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public CategoryHttpRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task DeleteCategory(string id)
        {
            var deleteResult = await _client.DeleteAsync($"homewallet/Category/{id}");
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();
            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }

        public async Task<PagingResponse<CategoryPwa>> GetCategories(PageParameters pageParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = pageParameters.PageNumber.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("homewallet/Category", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingCategories = new PagingResponse<CategoryPwa>
            {
                Items = JsonSerializer.Deserialize<List<CategoryPwa>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };
            return pagingCategories;
        }

        public async Task<CategoryPwa> GetCategory(string id)
        {
            var response = await _client.GetAsync($"homewallet/Category/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            return JsonSerializer.Deserialize<CategoryPwa>(content, _options);
        }

        public async Task<bool> CreateCategory(CategoryPwa category)
        {
            var content = JsonSerializer.Serialize(category);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync("homewallet/Category", bodyContent);
            if (postResult.StatusCode == HttpStatusCode.BadRequest)
            {
                return false;
            }

            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }

            return true;
        }

        public async Task<bool> UpdateCategory(CategoryPwa category)
        {
            var content = JsonSerializer.Serialize(category);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var putResult = await _client.PutAsync($"homewallet/Category", bodyContent);
            if (putResult.StatusCode == HttpStatusCode.BadRequest)
            {
                return false;
            }

            var putContent = await putResult.Content.ReadAsStringAsync();
            if (!putResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }

            return true;
        }
    }
}