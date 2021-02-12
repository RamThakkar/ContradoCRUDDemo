using API.Helper;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDemoContext _context;

        public ProductRepository(ECommerceDemoContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<Product> DeleteProductAsync(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductById(long id)
        {
            return await _context.Products.Include(p => p.ProdCat).FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(PagedParameters pagedParameters)
        {
            return await _context.Products.Skip((pagedParameters.PageNumber - 1) * pagedParameters.PageSize)
                .Take(pagedParameters.PageSize).Include(p => p.ProdCat).ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var updatedProduct = _context.Products.Attach(product);
            updatedProduct.State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return null;

            }
            return product;
        }
    }
}
