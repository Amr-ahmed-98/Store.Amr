using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services.Abstractions
{
    public interface IProductService
    {
        // Get All Product
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();

        // Get Product by Id
        Task<ProductResultDto?> GetProductByIdAsync(int id);

        // Get All Brand
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

        // Get All Types
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
    }
}
