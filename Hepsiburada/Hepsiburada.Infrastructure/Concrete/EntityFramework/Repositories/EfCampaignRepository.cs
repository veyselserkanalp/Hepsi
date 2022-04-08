using Hepsiburada.Domain.Entities;
using Hepsiburada.Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Hepsiburada.Infrastructure.Concrete.EntityFramework.Repositories
{
    public class EfCampaignRepository : EfEntityRepositoryBase<Campaign, int>, IEfCampaignRepository
    {
        public EfCampaignRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
