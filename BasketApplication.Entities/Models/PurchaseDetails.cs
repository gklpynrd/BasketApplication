namespace BasketApplication.Entities.Models
{
    public class PurchaseDetails
    {
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public PurchaseHistory Purchase { get; set; }
        public Product Product { get; set; }
    }
}
