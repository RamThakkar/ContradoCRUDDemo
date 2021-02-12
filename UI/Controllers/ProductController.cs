using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UI.Dtos;

namespace UI.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : Controller
    {


        private readonly IHttpClientFactory _client;
        

        public ProductController(IHttpClientFactory client)
        {
            _client = client;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Products = Enumerable.Empty<ProductDto>();
            HttpClient client = _client.CreateClient("myclient");
            var response = client.GetAsync("/api/products").Result;
            if (response.IsSuccessStatusCode)
            {
                Products = JsonConvert.DeserializeObject<IReadOnlyList<ProductDto>>(await response.Content.ReadAsStringAsync());
            }
            return Json(new { data= Products.ToList() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _client.CreateClient("myclient");
            var response = await client.DeleteAsync("/api/products/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            return Json(new { success = true, message = "Delete successful" });
        }
    }
}
