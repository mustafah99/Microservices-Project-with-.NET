using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnimimoMicroservices.NewOrderService.Models;

namespace AnimimoMicroservices.NewOrderService.Data
{
    public class AnimimoMicroservicesNewOrderServiceContext : DbContext
    {
        public AnimimoMicroservicesNewOrderServiceContext (DbContextOptions<AnimimoMicroservicesNewOrderServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Domain.Order> Order { get; set; }
        public DbSet<Models.Domain.OrderLine> OrderLine { get; set; }


    }
}
