using Hepsiburada.Domain.Entities;
using Hepsiburada.Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Hepsiburada.Infrastructure.Concrete.EntityFramework.Repositories
{
    public class EfOrderRepository : EfEntityRepositoryBase<Order, int>, IEfOrderRepository
    {
        public EfOrderRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
