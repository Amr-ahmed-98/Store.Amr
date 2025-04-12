using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation
{
    // API Controller
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        // endpoint : public non-static method
        [HttpGet] // endpoint : GET : URL = api/products
        public async Task<IActionResult> GetAllProducts()
        {
           var result = await serviceManager.ProductService.GetAllProductsAsync();
            if (result is null) return BadRequest(); // status Code : 400
            return Ok(result); // status Code : 200 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound(); // 404
            return Ok(result);
        }




    }
}
