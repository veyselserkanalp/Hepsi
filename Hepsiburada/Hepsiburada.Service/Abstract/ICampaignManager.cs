using Hepsiburada.Domain.Entities;
using Hepsiburada.Service.Model.Campaign;
using Hepsiburada.Shared.Results.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hepsiburada.Service.Abstract
{
    public interface ICampaignManager
    {
        Task<DataResult<Campaign>> GetByName(string CampaignName);
        Task<DataResult<Campaign>> Create(CampaignDto Campaign);
        Task<DataResult<List<Campaign>>> End();
    }
}
