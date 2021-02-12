using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Dtos
{
    public class ProductDto
    {
        public long ProductId { get; set; }
        
        [Required]
        public int ProdCatId { get; set; }
        [Required]
        public string ProdName { get; set; }
        [Required]
        public string ProdDescription { get; set; }
        public string ProdCatName { get; set; }
        public IEnumerable<ProductCategoryDto> CategoryList { get; set; }
    }
}
