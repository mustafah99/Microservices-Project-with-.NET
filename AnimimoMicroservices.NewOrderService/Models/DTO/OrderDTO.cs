using AnimimoMicroservices.NewOrderService.Models.Domain;
using AnimimoMicroservices.NewOrderService.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimimoMicroservices.NewOrderService.DTO
{
    public class OrderDTO
    {
        public string Identifier { get; set; }

        public string Customer { get; set; }

        public List<OrderLine> Items { get; set; } = new List<OrderLine>();
    }
}