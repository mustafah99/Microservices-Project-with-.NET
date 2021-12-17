#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnimimoMicroservices.CatalogService;

namespace AnimimoMicroservices.CatalogService.Data
{
    public class AnimimoMicroservicesCatalogServiceContext : DbContext
    {
        public AnimimoMicroservicesCatalogServiceContext (DbContextOptions<AnimimoMicroservicesCatalogServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AnimimoMicroservices.CatalogService.Catalog> Catalog { get; set; }
    }
}
