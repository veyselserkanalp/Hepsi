using Hepsiburada.Domain.Entities;
using Hepsiburada.Service.Model.Order;
using Hepsiburada.Shared.Results.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hepsiburada.Service.Abstract
{
    public interface IOrderManager
    {
        public Task<DataResult<Order>> Create(OrderDto orderDto);
        public Task<DataResult<List<Order>>> GetListByCampaignId(int campaignId);


    }
}
