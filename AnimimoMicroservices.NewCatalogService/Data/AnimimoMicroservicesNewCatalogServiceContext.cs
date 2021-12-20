#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnimimoMicroservices.StockService.Models.Domain;

namespace AnimimoMicroservices.NewCatalogService.Data
{
    public class AnimimoMicroservicesNewCatalogServiceContext : DbContext
    {
        public AnimimoMicroservicesNewCatalogServiceContext (DbContextOptions<AnimimoMicroservicesNewCatalogServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AnimimoMicroservices.StockService.Models.Domain.Catalog> Catalog { get; set; }
    }
}
