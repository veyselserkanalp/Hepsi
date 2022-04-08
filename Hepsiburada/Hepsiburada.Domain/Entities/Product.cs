using Hepsiburada.Shared.Abstract;

namespace Hepsiburada.Domain.Entities
{
    public class Product : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Stock { get; set; }
        public decimal CurrentPrice { get; set; }


    }
}
