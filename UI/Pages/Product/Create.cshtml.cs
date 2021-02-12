using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.Dtos;

namespace UI.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _client;

        public CreateModel(IHttpClientFactory client)
        {
            _client = client;
        }

        [BindProperty]
        public ProductDto Product { get; set; }

        public async Task OnGet()
        {
            HttpClient client = _client.CreateClient("myclient");            
            var response = client.GetAsync("/api/Category").Result;
            Product = new ProductDto();
            Product.CategoryList = JsonConvert.DeserializeObject<IReadOnlyList<ProductCategoryDto>>(await response.Content.ReadAsStringAsync());

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(Product), UnicodeEncoding.UTF8, "application/json");
                HttpClient client = _client.CreateClient("myclient");
                var response = await client.PostAsync("/api/products", content);
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
