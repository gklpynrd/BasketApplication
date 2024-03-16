namespace BasketApplication.Entities.Dtos.ProductDto
{
    public class CreateProductRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
