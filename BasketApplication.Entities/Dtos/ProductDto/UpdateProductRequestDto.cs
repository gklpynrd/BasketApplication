namespace BasketApplication.Entities.Dtos.ProductDto
{
    public class UpdateProductRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
