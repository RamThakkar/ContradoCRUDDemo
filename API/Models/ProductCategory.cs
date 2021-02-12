using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            ProductAttributeLookups = new HashSet<ProductAttributeLookup>();
            Products = new HashSet<Product>();
        }

        public int ProdCatId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<ProductAttributeLookup> ProductAttributeLookups { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
