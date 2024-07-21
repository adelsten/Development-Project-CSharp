using Microsoft.AspNetCore.Mvc;
using Sparcpoint;
using Sparcpoint.Abstract;
using Sparcpoint.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Interview.Web.Controllers
{
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private IDataSerializer _serializer;
        private IProductService _productService;

        public ProductController(IDataSerializer dataSerializer, IProductService productService)
        {
            _serializer = dataSerializer;
            _productService = productService;
        }

        // NOTE: Sample Action
        [HttpGet]
        public Task<IActionResult> GetAllProducts()
        {
            return Task.FromResult((IActionResult)Ok(_productService.GetAllProducts()));
        }

        [HttpPost]
        [Route("AddProduct")]
        public Task<IActionResult> AddProduct([FromBody] object productRequest)
        {
            var product = (Product)_serializer.Deserialize(typeof(Product), productRequest.ToString());
            return Task.FromResult((IActionResult)Ok(_productService.AddProduct(product)));
        }
    }
}
