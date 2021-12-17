using System.ComponentModel.DataAnnotations;

namespace AnimimoMicroservices.CatalogService
{
    public class Catalog
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? imageUrl { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string? ArticleNumber { get; set; }

        [Required]
        public string? urlSlug { get; set; }
    }
}
