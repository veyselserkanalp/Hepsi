using Hepsiburada.Infrastructure;
using Hepsiburada.Infrastructure.Concrete.EntityFramework.Context;
using Hepsiburada.Service.Abstract;
using Hepsiburada.Service.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hepsiburada.IO
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider;
        public static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<HepsiburadaContext>(options =>
            {
                options.UseSqlServer(GetDbConnectionByName("HepsiBuradaConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<ICampaignManager, CampaignManager>();

            services.AddHostedService<BackgroundService>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public static void DisposeServices()
        {
            if (ServiceProvider == null)
            {
                return;
            }
            if (ServiceProvider is IDisposable)
            {
                ((IDisposable)ServiceProvider).Dispose();
            }
        }

        private static string GetDbConnectionByName(string ConnectionName)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            return configuration.GetConnectionString(ConnectionName);
        }
    }
}
