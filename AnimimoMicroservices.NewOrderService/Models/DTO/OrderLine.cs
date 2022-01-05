using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AnimimoMicroservices.NewOrderService.Models.DTO
{
    public class OrderLine
    {
        [Key]
        public int OrderID { get; set; }
        public string Identifier { get; set; }

        public string Customer { get; set; }

        public OrderLine(string identifier)
        {
            Identifier = identifier;
        }
        public IEnumerable<BasketEntryDto> Items { get; set; } = new List<BasketEntryDto>();
    }
}
