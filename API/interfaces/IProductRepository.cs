using API.Helper;
using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(long id);
        Task<IReadOnlyList<Product>> GetProductsAsync(PagedParameters pagedParameters);

        Task<Product> AddProductAsync(Product product);

        Task<Product> UpdateProductAsync(Product product);

        Task<Product> DeleteProductAsync(long id);

    }
}
