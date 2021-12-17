using Microsoft.AspNetCore.Mvc;

namespace AnimimoMicroservices.StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpPut("{articleNumber}")]
        public IActionResult UpdateStockLevel(string articleNumber, UpdateStockLevelDto updateStockLevelDto)
        {
            return NoContent();
        }
    }
}
