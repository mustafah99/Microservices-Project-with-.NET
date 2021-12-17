using AnimimoMicroservices.StockService.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AnimimoMicroservices.StockService.Data

{
    public class StockServiceContext : DbContext
    {
        public DbSet<StockLevel> StockLevel { get; set; }

        public StockServiceContext()
        {

        }
    }
}
