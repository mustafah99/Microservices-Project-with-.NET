using System.ComponentModel.DataAnnotations;

namespace AnimimoMicroservices.StockService.Models.Domain
{
    public class Catalog
    {
        public Catalog(int id, string name, string description, string imageUrl, int price, string articleNumber, string urlSlug)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            ArticleNumber = articleNumber;
            UrlSlug = urlSlug;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string ArticleNumber { get; set; }
        public string UrlSlug { get; set; }

    }
}
