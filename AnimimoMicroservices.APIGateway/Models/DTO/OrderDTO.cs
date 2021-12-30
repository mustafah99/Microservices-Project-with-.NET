using System.ComponentModel.DataAnnotations;

namespace AnimimoMicroservices.APIGateway.DTO
{
    public class OrderDTO
    {
        [Key]
        public int OrderID { get; set; }
        public string Identifier { get; set; }
        public string Customer { get; set; }
        public IEnumerable<NewBasketDTO> Basket { get; set; } = new List<NewBasketDTO>();
    }
}