namespace BasketApplication.Entities.Models
{
    public class PurchaseHistory
    {
        public int PurchaseId { get; set; }
        public string UserId { get; set; }
        public int DetailsId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public AppUser User { get; set; }
        public List<PurchaseDetails> Details { get; set; }
    }
}
