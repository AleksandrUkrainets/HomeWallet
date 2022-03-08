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
    public class OperationHttpRepository : IOperationHttpRepository
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public OperationHttpRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<bool> CreateOperation(OperationPwa operation)
        {
            var content = JsonSerializer.Serialize(operation);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync("homewallet/Operation", bodyContent);
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

        public async Task DeleteOperation(string id)
        {
            var deleteResult = await _client.DeleteAsync($"homewallet/Operation/{id}");
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();
            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }

        public async Task<OperationPwa> GetOperation(string id)
        {
            var response = await _client.GetAsync($"homewallet/Operation/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            return JsonSerializer.Deserialize<OperationPwa>(content, _options);
        }

        public async Task<bool> UpdateOperation(OperationPwa operation)
        {
            var content = JsonSerializer.Serialize(operation);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var putResult = await _client.PutAsync($"homewallet/Operation/{operation.Id}", bodyContent);
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

        public async Task<PagingResponse<OperationPwa>> GetOperations(PageParameters pageParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = pageParameters.PageNumber.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("homewallet/Operation", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingOperations = new PagingResponse<OperationPwa>
            {
                Items = JsonSerializer.Deserialize<List<OperationPwa>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingOperations;
        }
    }
}