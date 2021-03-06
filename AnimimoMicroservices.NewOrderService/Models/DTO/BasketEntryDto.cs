using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimimoMicroservices.NewOrderService.Models.DTO
{
    public class BasketEntryDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}