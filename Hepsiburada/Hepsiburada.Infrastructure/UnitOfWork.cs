using Hepsiburada.Infrastructure.Abstract;
using Hepsiburada.Infrastructure.Concrete.EntityFramework.Context;
using Hepsiburada.Infrastructure.Concrete.EntityFramework.Repositories;
using System;
using System.Threading.Tasks;

namespace Hepsiburada.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private HepsiburadaContext _hepsiburadaContext { get; set; }

        private readonly EfProductRepository _efProductRepository;
        private readonly EfOrderRepository _efOrderRepository;
        private readonly EfCampaignRepository _efCampaignRepository;

        public UnitOfWork(HepsiburadaContext hepsiburadaContext)
        {
            _hepsiburadaContext = hepsiburadaContext;
        }

        public IEfProductRepository ProductRepository => _efProductRepository ?? new EfProductRepository(_hepsiburadaContext);

        public IEfOrderRepository OrderRepository => _efOrderRepository ?? new EfOrderRepository(_hepsiburadaContext);

        public IEfCampaignRepository CampaignRepository => _efCampaignRepository ?? new EfCampaignRepository(_hepsiburadaContext);

        public async Task<int> SaveAsync()
        {
            return await _hepsiburadaContext.SaveChangesAsync();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _hepsiburadaContext.Dispose();
                }
            }

            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
