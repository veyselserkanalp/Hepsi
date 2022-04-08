using Hepsiburada.Domain.Entities;
using Hepsiburada.Infrastructure;
using Hepsiburada.Service.Abstract;
using Hepsiburada.Service.Model.Product;
using Hepsiburada.Shared.ComplexTypes;
using Hepsiburada.Shared.Results.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiburada.Service.Concrete
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<DataResult<Product>> Create(ProductDto createProductDto)
        {
            Product product = new();
            product.Name = createProductDto.Name;
            product.ProductCode = createProductDto.ProductCode;
            product.Stock = createProductDto.Stock;
            product.CurrentPrice = createProductDto.CurrentPrice;
            var responseProduct = await _unitOfWork.ProductRepository.AddAsync(product);
            var result = await _unitOfWork.ProductRepository.SaveChangesAsync();

            if (result > 0)
            {
                return new DataResult<Product>(ResultStatus.Success, "İşleminizi başarıyla kayıt edildi.", responseProduct, true);
            }
            else
            {
                return new DataResult<Product>(ResultStatus.Error, "İşleminizi başarıyla kayıt edilmemiştir.", false);
            }
        }

        public async Task<DataResult<IEnumerable<Product>>> GetAll()
        {
            var allProducts = await _unitOfWork.ProductRepository.GetAllAsync();
            if (allProducts.Any())
            {
                return new DataResult<IEnumerable<Product>>(ResultStatus.Success, "İşleminizi başarıyla kayıt edildi.", allProducts, true);
            }
            else
            {
                return new DataResult<IEnumerable<Product>>(ResultStatus.DataNull, "İşleminizi ilgili herhangi bir kayda erişilememiştir.", false);
            }
        }

        public async Task<DataResult<Product>> GetByCode(string productCode)
        {
            var result = _unitOfWork.ProductRepository.GetAllAsync().Result.FirstOrDefault(p => p.ProductCode == productCode);
            if (result != null)
            {
                return new DataResult<Product>(ResultStatus.Success, "İşleminizi başarıyla kayıt edildi.", result, true);
            }
            else
            {
                return new DataResult<Product>(ResultStatus.Error, "İşleminizi başarıyla kayıt edilmemitir.", false);
            }
        }

        public async Task<DataResult<Product>> GetById(int productId)
        {
            var result = _unitOfWork.ProductRepository.GetAllAsync().Result.FirstOrDefault(p => p.Id == productId);
            if (result != null)
            {
                return new DataResult<Product>(ResultStatus.Success, "İşleminizi başarıyla kayıt edildi.", result, true);
            }
            else
            {
                return new DataResult<Product>(ResultStatus.Error, "İşleminizi başarıyla kayıt edilmemitir.", false);
            }
        }
    }
}
