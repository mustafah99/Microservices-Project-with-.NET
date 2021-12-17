#nullable disable
using Microsoft.EntityFrameworkCore;

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
