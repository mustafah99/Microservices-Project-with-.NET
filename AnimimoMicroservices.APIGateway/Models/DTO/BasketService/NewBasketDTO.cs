namespace AnimimoMicroservices.APIGateway.DTO.BasketService
{
    public class ItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    [ActivatorUtilitiesConstructor]
    public class NewBasketDTO
    {
        public string Identifier { get; set; }
        public List<ItemDTO> Items { get; set; }
    }

    public class JournalDto
    {
        public int Id { get; set; }
        public IEnumerable<NewBasketDTO> Entries { get; set; } = new List<NewBasketDTO>();
    }
}