using BasketApplication.Entities.Dtos.ProductDto;

namespace BasketApplication.Entities.Dtos.Purchase
{
    public class PurchaseHistoryDto
    {
        public int PurchaseId { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<HistoryProductDto> Products { get; set; }

    }
}
