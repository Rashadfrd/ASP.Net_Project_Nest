namespace NestProject.Models
{
    public class Badge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string Color { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
