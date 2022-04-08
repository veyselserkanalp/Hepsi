using Hepsiburada.Infrastructure.Abstract;
using System.Threading.Tasks;

namespace Hepsiburada.Infrastructure
{
    public interface IUnitOfWork
    {
        public IEfProductRepository ProductRepository { get; }
        public IEfOrderRepository OrderRepository { get; }
        public IEfCampaignRepository CampaignRepository { get; }
        Task<int> SaveAsync();

    }
}