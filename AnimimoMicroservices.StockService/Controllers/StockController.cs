using AnimimoMicroservices.StockService.Data;
using AnimimoMicroservices.StockService.Models.Domain;
using AnimimoMicroservices.StockService.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AnimimoMicroservices.StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        public StockController(StockServiceContext context)
        {
            Context = context;
        }
        private StockServiceContext Context { get; }

        [HttpPut("{articleNumber}")]
        public IActionResult UpdateStockLevel(string articleNumber, UpdateStockLevelDto updateStockLevelDto)
        {
            var stockLevel = Context.StockLevel.
                FirstOrDefault(x => x.ArticleNumber == updateStockLevelDto.ArticleNumber);

            if (stockLevel == null)
            {
                stockLevel = new StockLevel(
                    updateStockLevelDto.ArticleNumber,
                    updateStockLevelDto.StockLevel
                    );

                Context.StockLevel.Add(stockLevel);

            }
            else
            {
                stockLevel.Stock = updateStockLevelDto.StockLevel;
            }

            Context.SaveChanges();

            return NoContent(); // Returns 204 No Content
        }

        [HttpGet]
        public IEnumerable<StockLevelDto> GetAll()
        {
            var stockLevelDtos = Context.StockLevel.Select(x => new StockLevelDto
            {
                ArticleNumber = x.ArticleNumber,
                Stock = x.Stock
            });

            return stockLevelDtos;
        }
    }

    public class StockLevelDto
    {
        public string ArticleNumber { get; set; }
        public int Stock { get; set; }
    }
}