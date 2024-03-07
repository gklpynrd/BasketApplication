namespace BasketApplication.Entities.Dtos.Product
{
    public class CreateProductRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
