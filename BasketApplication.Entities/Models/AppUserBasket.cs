namespace BasketApplication.Entities.Models
{
    public class AppUserBasket
    {
        public string AppUserId { get; set; }
        public int ProductId { get; set; }
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }

    }
}