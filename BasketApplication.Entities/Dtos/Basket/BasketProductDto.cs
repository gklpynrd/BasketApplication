namespace BasketApplication.Entities.Dtos.Basket
{
    public class BasketProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalProductPrice { get; set; }
    }
}
