namespace AnimimoMicroservices.APIGateway.DTO
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
}