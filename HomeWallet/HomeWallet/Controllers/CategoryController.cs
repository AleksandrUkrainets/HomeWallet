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
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] PageParameters pageParameters)
        {
            var categories = await _categoryService.GetCategories(pageParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(categories.MetaData));

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return Ok(await _categoryService.GetCategory(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO category)
        {
            await _categoryService.CreateCategory(category);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO category)
        {
            await _categoryService.UpdateCategory(category);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);

            return Ok();
        }
    }
}