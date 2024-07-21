using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sparcpoint;
using Sparcpoint.Abstract;
using Sparcpoint.Models;
using Sparcpoint.Services;
using System.Threading.Tasks;

namespace Interview.Web.Controllers
{
    [Route("api/v1/categories")]
    public class CategoryController : Controller
    {
        private IDataSerializer _serializer;
        private ICategoryService _categoryService;

        public CategoryController(IDataSerializer serializer, ICategoryService categoryService)
        {
            _serializer = serializer;
            _categoryService = categoryService;
        }

        public Task<IActionResult> GetAllCategories()
        {
            return Task.FromResult((IActionResult)Ok(_categoryService.GetCategories()));
        }

        [HttpPost]
        [Route("AddCategory")]
        public Task<IActionResult> AddCategory([FromBody] object productRequest)
        {
            var category = (Category)_serializer.Deserialize(typeof(Category), productRequest.ToString());
            return Task.FromResult((IActionResult)Ok(_categoryService.AddCategory(category)));
        }
    }
}
