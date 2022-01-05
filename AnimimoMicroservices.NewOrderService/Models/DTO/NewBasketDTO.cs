using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimimoMicroservices.NewOrderService.DTO
{
    [Keyless]
    [NotMapped]
    public class NewBasketDTO
    {
        public class ItemDTO
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        [ActivatorUtilitiesConstructor]
        public class BasketDTO
        {
            public string Identifier { get; set; }
            public List<ItemDTO> Items { get; set; }
        }

    }
}