namespace NestProject.Models
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FoundedYear { get; set; }
        public bool IsDeleted { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public string BgImageUrl { get; set; }
        public string ProfileImageUrl { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
