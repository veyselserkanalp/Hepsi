using Hepsiburada.Shared.Abstract;
using System;

namespace Hepsiburada.Domain.Entities
{
    public class Order : IEntity<int>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }

        public int? CampaignId { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Quantity { get; set; }

    }
}
