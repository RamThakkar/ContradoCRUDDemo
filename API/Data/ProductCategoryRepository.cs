using API.Helper;
using API.interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ECommerceDemoContext _context;

        public ProductCategoryRepository(ECommerceDemoContext context)
        {
            _context = context;
        }

        public async Task<ProductCategory> AddCategoryAsync(ProductCategory category)
        {
            _context.ProductCategories.Add(category);
            await _context.SaveChangesAsync();
            return category;

        }

        public async Task<ProductCategory> DeleteCategoryAsync(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            if (category == null)
            {
                return null;
            }

            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<ProductCategory> GetCategoryById(int id)
        {
            return await _context.ProductCategories.FindAsync(id);
        }

        public async Task<IReadOnlyList<ProductCategory>> GetCategoryAsync(PagedParameters pagedParameters)
        {
            return await _context.ProductCategories.Skip((pagedParameters.PageNumber - 1) * pagedParameters.PageSize)
                .Take(pagedParameters.PageSize).ToListAsync();

        }

        public async Task<ProductCategory> UpdateCategoryAsync(ProductCategory category)
        {
            var updatedcategory = _context.ProductCategories.Attach(category);
            updatedcategory.State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return null;

            }
            return category;
        }
    }
}
