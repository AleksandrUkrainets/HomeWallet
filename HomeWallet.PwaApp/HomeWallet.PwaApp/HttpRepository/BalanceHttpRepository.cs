using HomeWallet.PwaApp.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp.HttpRepository
{
    public class BalanceHttpRepository : IBalanceHttpRepository
    {
        private readonly HttpClient _client;
        private const string _formatDate = "MM/dd/yyyy HH:mm";

        public BalanceHttpRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<BalancePwa> GetDailyBalance(DateTime date)
        {
            var response = await _client.GetAsync($"homewallet/Balance/dailybalance?date={date.ToString(_formatDate)}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var dayliBalance = JsonConvert.DeserializeObject<BalancePwa>(content);

            return dayliBalance;
        }

        public async Task<BalancePwa> GetRangeBalance(DateTime sinceDate, DateTime tillDate)
        {
            var response = await _client.GetAsync($"homewallet/Balance/rangebalance?sinceDate={sinceDate.ToString(_formatDate)}&tillDate={tillDate.ToString(_formatDate)}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var rangeBalance = JsonConvert.DeserializeObject<BalancePwa>(content);

            return rangeBalance;
        }
    }
}