using Hepsiburada.Service.Abstract;
using Hepsiburada.Service.Model.Order;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hepsiburada.IO
{
    public class BackgroundService : IHostedService, IDisposable
    {
        private Timer _timerOrderManager;
        private Timer _timerCampaignManager;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timerOrderManager = new Timer(PurchaseOrderRequest, null, TimeSpan.Zero,
             TimeSpan.FromSeconds(15));

            _timerCampaignManager = new Timer(EndCampaignsByEndDate, null, TimeSpan.Zero,
             TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timerOrderManager?.Change(Timeout.Infinite, 0);
            _timerCampaignManager?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timerOrderManager?.Dispose();
            _timerCampaignManager?.Dispose();
        }
        private void PurchaseOrderRequest(object state)
        {
            Random random = new Random();
            OrderDto orderDto = new();


            orderDto.ProductCode = "P" + random.Next(1, 77).ToString();
            orderDto.Quantity = random.Next(1, 20);
            orderDto.CreatedDate = DateTime.Now;


            var orderService = Startup.ServiceProvider.GetService<IOrderManager>();
            var serviceResult = orderService.Create(orderDto);

            if (serviceResult.Result.Success)
                Console.WriteLine($"Order is created; Product Code : {serviceResult.Result.Data.ProductCode} , Quantity : {serviceResult.Result.Data.Quantity}");

        }
        private void EndCampaignsByEndDate(object state)
        {
            var campaignService = Startup.ServiceProvider.GetService<ICampaignManager>();
            campaignService.End();
        }
    }
}
