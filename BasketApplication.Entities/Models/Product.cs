using System.ComponentModel.DataAnnotations.Schema;

namespace BasketApplication.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "Decimal(18,2)")]
        public decimal Price { get; set; }
        public ProductStock Stock { get; set; }
        public List<AppUserBasket> AppUserBaskets { get; set; }
    }
}
