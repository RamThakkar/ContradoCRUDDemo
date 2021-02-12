using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UI.Dtos;

namespace UI.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _client;

        public IndexModel(IHttpClientFactory client)
        {
            _client = client;
        }

        public IEnumerable<ProductDto> Products { get; set; }
        public async Task OnGet()
        {
            HttpClient client = _client.CreateClient("myclient");
            var response = client.GetAsync("/api/products").Result;
            if (response.IsSuccessStatusCode)
            {
                Products = JsonConvert.DeserializeObject<IReadOnlyList<ProductDto>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Products = Enumerable.Empty<ProductDto>();
            }
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {   
            HttpClient client = _client.CreateClient("myclient");
            await client.DeleteAsync("/api/products/"+id);
            return RedirectToPage("Index");
        }
    }
}
