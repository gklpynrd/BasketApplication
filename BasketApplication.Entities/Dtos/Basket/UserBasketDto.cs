namespace BasketApplication.Entities.Dtos.Basket
{
    public class UserBasketDto
    {
        public string Username { get; set; }
        public decimal TotalBasketPrice { get; set; }
        public List<BasketProductDto> Products { get; set; }

    }
}
