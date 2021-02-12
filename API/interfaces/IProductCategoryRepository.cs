using API.Helper;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.interfaces
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategory> GetCategoryById(int id);
        Task<IReadOnlyList<ProductCategory>> GetCategoryAsync(PagedParameters pagedParameters);

        Task<ProductCategory> AddCategoryAsync(ProductCategory product);

        Task<ProductCategory> UpdateCategoryAsync(ProductCategory product);

        Task<ProductCategory> DeleteCategoryAsync(int id);
    }
}
