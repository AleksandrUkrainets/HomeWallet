using HomeWallet.Application;
using HomeWallet.Models;
using HomeWallet.Models.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HomeWallet.Server.Controllers
{
    [ApiController]
    [Route("api/homewallet/[controller]")]
    public class OperationController : Controller
    {
        private readonly OperationService _operationService;

        public OperationController(OperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOperations([FromQuery] PageParameters pageParameters)
        {
            var operations = await _operationService.GetOperations(pageParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(operations.MetaData));

            return Ok(operations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperation(int id)
        {
            return Ok(await _operationService.GetOperation(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOperation([FromBody] OperationDTO operation)
        {
            await _operationService.CreateOperation(operation);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOperation([FromBody] OperationDTO newOperation)
        {
            await _operationService.UpdateOperation(newOperation);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            await _operationService.DeleteOperation(id);

            return Ok();
        }
    }
}