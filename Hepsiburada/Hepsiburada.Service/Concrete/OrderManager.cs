using Hepsiburada.Domain.Entities;
using Hepsiburada.Infrastructure;
using Hepsiburada.Service.Abstract;
using Hepsiburada.Service.Model.Order;
using Hepsiburada.Shared.ComplexTypes;
using Hepsiburada.Shared.Results.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiburada.Service.Concrete
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DataResult<Order>> Create(OrderDto orderDto)
        {

            var product = _unitOfWork.ProductRepository.GetAllAsync().Result.FirstOrDefault(p => p.ProductCode == orderDto.ProductCode);

            if (product == null)
            {
                return new DataResult<Order>(ResultStatus.DataNull, "Ürüne bilgisine erişilememiştir.", false);
            }
            var campaign = _unitOfWork.CampaignRepository.GetAllAsync().Result.FirstOrDefault(p => p.ProductId == orderDto.ProductId);
            if (campaign == null)
            {
                return new DataResult<Order>(ResultStatus.DataNull, "Ürünün kampanaya bilgisine erişilememiştir.", false);
            }
            else
            {
                Order order = new();
                order.UnitPrice = orderDto.UnitPrice;
                order.TotalPrice = orderDto.TotalPrice;
                order.Discount = orderDto.Discount;
                order.CampaignId = orderDto.CampaignId;
                order.ProductId = orderDto.ProductId;
                order.ProductCode = orderDto.ProductCode;
                var responseOrder = await _unitOfWork.OrderRepository.AddAsync(order);

                product.Stock = product.Stock - orderDto.Quantity;

                await _unitOfWork.ProductRepository.UpdateAsync(product);

                var result = await _unitOfWork.SaveAsync();
                if (result > 0)
                {
                    return new DataResult<Order>(ResultStatus.Success, "İşleminizi başarıyla kayıt edildi.", responseOrder, true);
                }
                else
                {
                    return new DataResult<Order>(ResultStatus.Error, "İşleminizi başarıyla kayıt edilmemiştir.", false);
                }
            }
        }

        public async Task<DataResult<List<Order>>> GetListByCampaignId(int campaignId)
        {
            var result = _unitOfWork.OrderRepository.GetAllAsync().Result.Where(p => p.CampaignId == campaignId).ToList();
            if (result.Any())
            {
                return new DataResult<List<Order>>(ResultStatus.Success, "İşleminizi başarıyla kayıt edildi.", result, true);
            }
            else
            {
                return new DataResult<List<Order>>(ResultStatus.DataNull, "İşleminizi ilgili herhangi bir kayda erişilememiştir.", false);
            }
        }
    }
}
