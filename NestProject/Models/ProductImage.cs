namespace NestProject.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool? Status { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
