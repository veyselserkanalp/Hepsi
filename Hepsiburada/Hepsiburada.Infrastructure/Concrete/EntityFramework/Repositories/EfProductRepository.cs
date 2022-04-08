using Hepsiburada.Domain.Entities;
using Hepsiburada.Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Hepsiburada.Infrastructure.Concrete.EntityFramework.Repositories
{
    public class EfProductRepository : EfEntityRepositoryBase<Product, int>, IEfProductRepository
    {
        public EfProductRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
