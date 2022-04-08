using Hepsiburada.IO.Model;
using Hepsiburada.Service.Abstract;
using Hepsiburada.Service.Model.Campaign;
using Hepsiburada.Service.Model.Order;
using Hepsiburada.Service.Model.Product;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Hepsiburada.IO
{
    public class DispatcherEngine
    {
        public static Dictionary<string, Delegate> _CommandSet;
        public static Dictionary<string, Delegate> CommandSet
        {
            get
            {
                if (_CommandSet == null)
                {
                    _CommandSet = new Dictionary<string, Delegate>();

                    //Product
                    _CommandSet.Add("create_product", new DelegateCreateProduct(DispatcherEngine.CreateProduct));
                    _CommandSet.Add("get_product_info", new DelegateGetProductInfo(DispatcherEngine.GetProductInfo));

                    //Order
                    _CommandSet.Add("create_order", new DelegateCreateOrder(DispatcherEngine.CreateOrder));

                    //Campaign
                    _CommandSet.Add("create_campaign", new DelegateCreateCampaign(DispatcherEngine.CreateCampaign));
                    _CommandSet.Add("get_campaign_info", new DelegateGetCampaignInfo(DispatcherEngine.GetCampaignInfo));

                    //Application 
                    _CommandSet.Add("help_all", new DelegateGetHelpAll(DispatcherEngine.GetHelpAll));
                    _CommandSet.Add("help", new DelegateGetHelpByCommand(DispatcherEngine.GetHelpByCommand));
                    _CommandSet.Add("clear", new DelegateClear(DispatcherEngine.Clear));
                    _CommandSet.Add("exit", new DelegateExit(DispatcherEngine.Exit));
                    _CommandSet.Add("start_time", new DelegateStartTime(DispatcherEngine.StartTime));
                    _CommandSet.Add("end_time", new DelegateEndTime(DispatcherEngine.EndTime));
                }
                return _CommandSet;
            }
        }
        #region Delegetes
        public delegate ResponseModel DelegateCreateProduct(string productCode, string productName, decimal unitPrice, int unitInStock);
        public delegate ResponseModel DelegateGetProductInfo(string productCode);

        public delegate ResponseModel DelegateCreateOrder(string productCode, int quantity);

        public delegate ResponseModel DelegateCreateCampaign(string campaignName, string productCode, decimal discountPercent, int duration, decimal limit, int targetSalesCount);
        public delegate ResponseModel DelegateGetCampaignInfo(string campaignName);

        public delegate ResponseModel DelegateGetHelpAll();
        public delegate ResponseModel DelegateGetHelpByCommand(string command);
        public delegate void DelegateClear();
        public delegate ResponseModel DelegateExit();
        public delegate void DelegateStartTime();
        public delegate void DelegateEndTime();
        #endregion

        #region Methods
        public static ResponseModel CreateProduct(string productCode, string name, decimal currentPrice, int stock)
        {
            var model = new ResponseModel();
            ProductDto productDto = new();

            productDto.ProductCode = productCode;
            productDto.Name = name;
            productDto.CurrentPrice = currentPrice;
            productDto.Stock = stock;

            var productService = Startup.ServiceProvider.GetService<IProductManager>();
            var serviceResult = productService.Create(productDto).Result;

            if (serviceResult.Success)
                model.Message = $"Product is created; Product Code : {serviceResult.Data.ProductCode} , Product Name : {serviceResult.Data.Name} Price : {serviceResult.Data.CurrentPrice}, Stock : {serviceResult.Data.Stock}";
            else
                model.Message = serviceResult.Message;

            return model;
        }

        public static ResponseModel GetProductInfo(string productCode)
        {
            var model = new ResponseModel();
            var productService = Startup.ServiceProvider.GetService<IProductManager>();
            var serviceResult = productService.GetByCode(productCode).Result;

            if (serviceResult.Success)
                model.Message = $"Product {serviceResult.Data.ProductCode} info; price {serviceResult.Data.CurrentPrice}, stock {serviceResult.Data.Stock}";
            else
                model.Message = serviceResult.Message;

            return model;
        }

        public static ResponseModel CreateOrder(string productCode, int quantity)
        {
            var model = new ResponseModel();
            OrderDto orderDto = new();

            orderDto.ProductCode = productCode;
            orderDto.Quantity = quantity;
            orderDto.CreatedDate = DateTime.Now;


            var orderService = Startup.ServiceProvider.GetService<IOrderManager>();
            var serviceResult = orderService.Create(orderDto).Result;

            if (serviceResult.Success)
                model.Message = $"Order is created; Product Code : {serviceResult.Data.ProductCode} , Quantity : {serviceResult.Data.Quantity}";
            else
                model.Message = serviceResult.Message;

            return model;
        }

        public static ResponseModel CreateCampaign(string campaignName, string productCode, decimal discountPercent, int duration, decimal limit, int targetSalesCount)
        {
            var model = new ResponseModel();
            CampaignDto campaignDto = new();


            campaignDto.ProductCode = productCode;
            campaignDto.PriceManipulationLimit = limit;
            campaignDto.Name = campaignName;
            campaignDto.Duration = duration;
            campaignDto.DiscountPercent = discountPercent;
            campaignDto.TargetSalesCount = targetSalesCount;

            var campaignService = Startup.ServiceProvider.GetService<ICampaignManager>();
            var serviceResult = campaignService.Create(campaignDto).Result;

            if (serviceResult.Success)
                model.Message = $"Campaign is created; name {serviceResult.Data.Name}, product {serviceResult.Data.ProductCode}, begin date {serviceResult.Data.BeginDate},end date {serviceResult.Data.EndDate} limit {serviceResult.Data.PriceManipulationLimit}, target sales count {serviceResult.Data.TargetSalesCount} ";
            else
                model.Message = serviceResult.Message;

            return model;
        }

        public static ResponseModel GetCampaignInfo(string campaignName)
        {
            var model = new ResponseModel();
            var campaignService = Startup.ServiceProvider.GetService<ICampaignManager>();
            var serviceResult = campaignService.GetByName(campaignName).Result;

            if (serviceResult.Success)
                model.Message = $"Campaign {serviceResult.Data.Name} info; , Target Sales {serviceResult.Data.TargetSalesCount}, Total Sales 50, Average Item Price 100 ";
            else
                model.Message = serviceResult.Message;

            return model;
        }


        public static Dictionary<string, string> _HelpMenu;

        public static Dictionary<string, string> HelpMenu
        {
            get
            {
                if (_HelpMenu == null)
                {
                    _HelpMenu = new Dictionary<string, string>();

                    _HelpMenu.Add("create_product", "create_product PRODUCTCODE PRODUCTNAME PRICE STOCK|Creates product in your system with given product information.");
                    _HelpMenu.Add("get_product_info", "get_product_info PRODUCTCODE|Prints product information for given product code.");
                    _HelpMenu.Add("create_order", "create_order PRODUCTCODE QUANTITY|Creates order in your system with given information.");
                    _HelpMenu.Add("create_campaign", "create_campaign NAME PRODUCTCODE DISCOUNTPERCENT DURATION PMLIMIT TARGETSALESCOUNT|Creates campaign in your system with giveninformation");
                    _HelpMenu.Add("get_campaign_info", "get_campaign_info NAME|Prints campaign information for given campaign name");
                    _HelpMenu.Add("increase_time", "increase_time HOUR|Increases time in your system.");

                }
                return _HelpMenu;
            }
        }

        public static ResponseModel GetHelpAll()
        {
            string helpInfoMenu = string.Empty;
            helpInfoMenu += Environment.NewLine;
            foreach (var item in HelpMenu)
            {
                var values = item.Value.Split("|");
                helpInfoMenu += "Command Name : " + item.Key + Environment.NewLine + "How to use : " + values[0] + Environment.NewLine + "What it does : " + values[1] + Environment.NewLine + Environment.NewLine;
            }
            var helpInfoModel = new ResponseModel();
            helpInfoModel.Message = helpInfoMenu;
            return helpInfoModel;
        }

        public static ResponseModel GetHelpByCommand(string command)
        {
            string helpInfoMenu = string.Empty;
            string item = string.Empty;

            HelpMenu.TryGetValue(command, out item);

            if (!string.IsNullOrEmpty(item))
            {
                var values = item.Split("|");
                helpInfoMenu += "Command Name : " + command + Environment.NewLine + "How to use : " + values[0] + Environment.NewLine + "What it does : " + values[1];
            }
            else
                helpInfoMenu += "There is no such command.";

            var helpInfoModel = new ResponseModel();
            helpInfoModel.Message = helpInfoMenu;
            return helpInfoModel;
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static ResponseModel Exit()
        {
            var model = new ResponseModel();
            model.Message = "Çıkıyor...";
            return model;
        }
        public static void StartTime()
        {
            var hostedService = Startup.ServiceProvider.GetService<IHostedService>();
            hostedService.StartAsync(new System.Threading.CancellationToken());
        }

        public static void EndTime()
        {
            var hostedService = Startup.ServiceProvider.GetService<IHostedService>();
            hostedService.StopAsync(new System.Threading.CancellationToken());
        }
        #endregion
    }
}
