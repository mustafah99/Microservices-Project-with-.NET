using System.ComponentModel.DataAnnotations;

namespace AnimimoMicroservices.NewOrderService.Models.Domain
{
    public class Order
    {
        public Order(int orderID, string identifier, string customer)
        {
            OrderID = orderID;
            Identifier = identifier;
            Customer = customer;
        }

        public Order(string identifier, string customer)
        {
            Identifier = identifier;
            Customer = customer;
        }

        [Key]
        public int OrderID { get; set; }
        public string Identifier { get; set; }

        public string Customer { get; set; }

        //[NotMapped]
        public IEnumerable<OrderLine> Items { get; set; } = new List<OrderLine>();
    }
}
