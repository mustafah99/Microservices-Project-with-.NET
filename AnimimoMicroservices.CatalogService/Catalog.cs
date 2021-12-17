namespace AnimimoMicroservices.CatalogService
{
    public class Catalog
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? imageUrl { get; set; }
        public int Price { get; set; }
        public string? ArticleNumber { get; set; }
        public string? urlSlug { get; set; }
    }
}
