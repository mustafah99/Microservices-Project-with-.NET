using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnimimoMicroservices.NewOrderService.DTO;
using AnimimoMicroservices.NewOrderService.Models;

namespace AnimimoMicroservices.NewOrderService.Data
{
    public class AnimimoMicroservicesNewOrderServiceContext : DbContext
    {
        public AnimimoMicroservicesNewOrderServiceContext (DbContextOptions<AnimimoMicroservicesNewOrderServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AnimimoMicroservices.NewOrderService.DTO.OrderDTO> OrderDTO { get; set; }
        public DbSet<AnimimoMicroservices.NewOrderService.Models.DTO.OrderLine> OrderLine { get; set; }

    }
}
