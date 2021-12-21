using System.ComponentModel.DataAnnotations;

namespace AnimimoMicroservices.NewOrderService.DTO
{
    public class OrderDTO
    {
        [Key]
        public int OrderID { get; set; }
        public string Identifier { get; set; }
        public string Customer { get; set; }
    }
}