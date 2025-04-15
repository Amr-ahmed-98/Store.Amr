using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithCountSpecification :BaseSpecifications<Product , int>
    {
        public ProductWithCountSpecification(ProductSpecificationsParameters specsParams) 
            :base(
                  p =>
                  (string.IsNullOrEmpty(specsParams.Search) || p.Name.ToLower().Contains(specsParams.Search.ToLower()))
                  &&
                  (!specsParams.BrandId.HasValue || p.BrandId == specsParams.BrandId)
                        &&
                        (!specsParams.TypeId.HasValue || p.TypeId == specsParams.TypeId)
                 )
        {
            
        }
    }
}
