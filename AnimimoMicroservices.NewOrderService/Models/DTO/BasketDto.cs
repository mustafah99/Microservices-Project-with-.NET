namespace AnimimoMicroservices.NewOrderService.Models.DTO
{
    public class BasketDto
    {
        public string Identifier { get; set; }
        public IEnumerable<BasketEntryDto> Items { get; set; } = new List<BasketEntryDto>();
    }
}
