using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIS.Controllers
{
   
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _genericRepository;

        public ProductController(IGenericRepository<Product> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products= await _genericRepository.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _genericRepository.GetAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Not Found", StatusCode = 404 });
            }
            return Ok(product);
        }
    }
}
