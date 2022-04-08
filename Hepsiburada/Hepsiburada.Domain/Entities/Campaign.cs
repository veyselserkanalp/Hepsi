using Hepsiburada.Shared.Abstract;
using System;

namespace Hepsiburada.Domain.Entities
{
    public class Campaign : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int ProductCode { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }
        public int Duration { get; set; }
        public int TargetSalesCount { get; set; }
        public decimal PriceManipulationLimit { get; set; }
        public bool IsActive { get; set; }
    }
}
