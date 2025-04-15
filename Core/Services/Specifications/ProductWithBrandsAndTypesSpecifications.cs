using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(P => P.Id == id)
        {
            ApplyInclude();
        }

        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationsParameters specsParams)
            : base(
                  p => 
                  (string.IsNullOrEmpty(specsParams.Search) || p.Name.ToLower().Contains(specsParams.Search.ToLower())) 
                  &&
                  (!specsParams.BrandId.HasValue || p.BrandId == specsParams.BrandId)
                        &&
                        (!specsParams.TypeId.HasValue || p.TypeId == specsParams.TypeId)
                  )
        {
            ApplyInclude();

            ApplySorting(specsParams.Sort);

            ApplyPagination(specsParams.PageIndex, specsParams.PageSize);


        }

        private void ApplyInclude()
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }

        }
    }
}
