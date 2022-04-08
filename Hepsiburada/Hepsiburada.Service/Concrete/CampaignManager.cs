using Hepsiburada.Domain.Entities;
using Hepsiburada.Infrastructure;
using Hepsiburada.Service.Abstract;
using Hepsiburada.Service.Model.Campaign;
using Hepsiburada.Shared.ComplexTypes;
using Hepsiburada.Shared.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiburada.Service.Concrete
{
    public class CampaignManager : ICampaignManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public CampaignManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DataResult<Campaign>> Create(CampaignDto campaignDto)
        {

            var product = _unitOfWork.ProductRepository.GetAllAsync().Result.FirstOrDefault(p => p.ProductCode == campaignDto.ProductCode);

            if (product == null)
            {
                return new DataResult<Campaign>(ResultStatus.DataNull, "İşleminize ait ürün bilgisine ulaşılamadı.", false);
            }
            Campaign campaign = new();
            campaign.Name = campaignDto.Name;
            campaign.ProductId = product.Id;
            campaign.TargetSalesCount = campaignDto.TargetSalesCount;
            campaign.BeginDate = campaignDto.BeginDate;
            campaign.EndDate = campaignDto.EndDate;
            campaign.Duration = campaignDto.Duration;
            campaign.IsActive = true;
            campaign.PriceManipulationLimit = campaignDto.PriceManipulationLimit;
            campaign.Price = product.CurrentPrice - (product.CurrentPrice * campaignDto.DiscountPercent / 100);

            var resultCampagin = await _unitOfWork.CampaignRepository.AddAsync(campaign);

            var result = await _unitOfWork.CampaignRepository.SaveChangesAsync();
            if (result > 0)
            {
                return new DataResult<Campaign>(ResultStatus.Success, "İşleminizi başarıyla kayıt edildi.", resultCampagin, true);

            }
            else
            {
                return new DataResult<Campaign>(ResultStatus.Error, "İşleminizi başarıyla kayıt edilmemiştir.", false);

            }

        }

        public async Task<DataResult<List<Campaign>>> End()
        {

            var campaigns = _unitOfWork.CampaignRepository.GetAllAsync().Result.Where(p => p.EndDate >= DateTime.Now).ToList();
            foreach (var campaign in campaigns)
            {
                campaign.IsActive = false;
                var resultCampagin = await _unitOfWork.CampaignRepository.UpdateAsync(campaign);
            }
            var result = await _unitOfWork.CampaignRepository.SaveChangesAsync();
            if (result > 0)
            {
                return new DataResult<List<Campaign>>(ResultStatus.Success, "İşleminizi başarıyla güncellendi.", campaigns, true);

            }
            else
            {
                return new DataResult<List<Campaign>>(ResultStatus.Error, "İşleminizi başarıyla kayıt edilmemiştir.", false);

            }

        }

        public async Task<DataResult<Campaign>> GetByName(string name)
        {
            var campaign = _unitOfWork.CampaignRepository.GetAllAsync().Result.FirstOrDefault(p => p.Name == name);
            if (campaign == null)
            {
                return new DataResult<Campaign>(ResultStatus.DataNull, "İşleminizi ait herhangi bir kayda erişelememiştir.", false);

            }
            else
            {
                return new DataResult<Campaign>(ResultStatus.Success, "İşleminizi başarıyla ulaşıldı.", campaign, true);
            }
        }
    }
}
