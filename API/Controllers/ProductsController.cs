using API.Dtos;
using API.Helper;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] PagedParameters pagedParameters)
        {
            var products = await _repo.GetProductsAsync(pagedParameters);
            if (products.Count() == 0)
                return NotFound();
           

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(long id)
        {
            var product = await _repo.GetProductById(id);
            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<Product,ProductDto>(product));
        }
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var newProduct = await _repo.AddProductAsync(product);
                return Ok(newProduct);
            }
            else
            {
                var messages = string.Join(";", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return StatusCode(400, messages);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(long id)
        {
            var product = await _repo.DeleteProductAsync(id);
            if (product != null)
            {
                return Ok();
            }
            return NotFound();
        }


        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            var UpdatedProduct = await _repo.UpdateProductAsync(product);
            if (UpdatedProduct != null)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}