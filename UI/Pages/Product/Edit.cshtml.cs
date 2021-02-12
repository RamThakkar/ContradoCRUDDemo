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
    public class EditModel : PageModel
    {

        private readonly IHttpClientFactory _client;

        public EditModel(IHttpClientFactory client)
        {
            _client = client;
        }
        
        [BindProperty]
        public ProductDto Product { get; set; }

        public async Task OnGet(long id)
        {
            HttpClient client = _client.CreateClient("myclient");
            var response = client.GetAsync("/api/products/"+id).Result;
            Product = JsonConvert.DeserializeObject<ProductDto>(await response.Content.ReadAsStringAsync());
            client = _client.CreateClient("myclient");
            response = client.GetAsync("/api/Category").Result;
            Product.CategoryList = JsonConvert.DeserializeObject<IReadOnlyList<ProductCategoryDto>>(await response.Content.ReadAsStringAsync());

            
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(Product), UnicodeEncoding.UTF8, "application/json");
                HttpClient client = _client.CreateClient("myclient");
                var response = await client.PutAsync("/api/products", content);
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
