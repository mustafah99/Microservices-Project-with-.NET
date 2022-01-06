using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimimoMicroservices.NewOrderService.Models.Domain
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        //[ForeignKey("OrderId")]
        //public virtual Order Order { get; protected set; }
    }
}
