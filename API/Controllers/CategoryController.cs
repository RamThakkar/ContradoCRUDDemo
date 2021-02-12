using API.Helper;
using API.interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _repo;

        public CategoryController(IProductCategoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductCategory>>> GetCategories([FromQuery] PagedParameters pagedParameters)
        {
            var products = await _repo.GetCategoryAsync(pagedParameters);
            if (products.Count() == 0)
                return NotFound();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            var product = await _repo.GetCategoryById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult> CreateCategory(ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                var newCategory = await _repo.AddCategoryAsync(category);
                return Ok(newCategory);
            }
            else
            {
                var messages = string.Join(";", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return StatusCode(400, messages);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var product = await _repo.DeleteCategoryAsync(id);
            if (product != null)
            {
                return Ok();
            }
            return NotFound();
        }


        [HttpPut]
        public async Task<ActionResult> UpdateCategory(ProductCategory category)
        {
            var UpdatedProduct = await _repo.UpdateCategoryAsync(category);
            if (UpdatedProduct != null)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}