using System.ComponentModel.DataAnnotations;

namespace AnimimoMicroservices.APIGateway.DTO.OrderService
{
    public class OrderDTO
    {
        public string Identifier { get; set; }
        public string Customer { get; set; }
    }
}