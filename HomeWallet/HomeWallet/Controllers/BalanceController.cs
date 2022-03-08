using HomeWallet.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HomeWallet.Server.Controllers
{
    [Route("api/homewallet/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly BalanceService _balanceService;

        public BalanceController(BalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet, Route("dailybalance")]
        public async Task<IActionResult> GetDailyBalance(DateTime date)
        {
            return Ok(await _balanceService.GetDailyBalance(date));
        }

        [HttpGet, Route("rangebalance")]
        public async Task<IActionResult> GetRangeBalance(DateTime sinceDate, DateTime tillDate)
        {
            return Ok(await _balanceService.GetRangeBalance(sinceDate, tillDate));
        }
    }
}