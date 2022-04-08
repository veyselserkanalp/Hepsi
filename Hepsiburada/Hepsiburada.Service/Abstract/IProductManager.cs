using Hepsiburada.Domain.Entities;
using Hepsiburada.Service.Model.Product;
using Hepsiburada.Shared.Results.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hepsiburada.Service.Abstract
{
    public interface IProductManager
    {
        Task<DataResult<IEnumerable<Product>>> GetAll();
        Task<DataResult<Product>> Create(ProductDto createProductDto);
        Task<DataResult<Product>> GetById(int productId);
        Task<DataResult<Product>> GetByCode(string productCode);

    }
}
