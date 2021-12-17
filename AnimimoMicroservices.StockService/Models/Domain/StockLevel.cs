namespace AnimimoMicroservices.StockService.Models.Domain
{
    public class StockLevel
    {
        public StockLevel(string? articleNumber, int stock)
        {
            ArticleNumber = articleNumber;
            Stock = stock;
        }

        public string? ArticleNumber { get; set; }
        public int Stock { get; set; }
    }
}
