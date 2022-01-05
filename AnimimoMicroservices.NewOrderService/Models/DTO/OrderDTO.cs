using AnimimoMicroservices.NewOrderService.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimimoMicroservices.NewOrderService.DTO
{
    public class OrderDTO
    {
        [Key]
        public int OrderID { get; set; }
        public string Identifier { get; set; }

        public string Customer { get; set; }

        public OrderDTO(string identifier)
        {
            Identifier = identifier;
        }

        [NotMapped]
        public IEnumerable<BasketEntryDto> Items { get; set; } = new List<BasketEntryDto>();
    }
}