using AnimimoMicroservices.NewerBasketService.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace AnimimoMicroservices.NewerBasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        public BasketController(IDistributedCache cache)
        {
            Cache = cache;
        }

        public IDistributedCache Cache { get; }

        [HttpPost]
        public IActionResult NewBasket(NewBasketDTO newBasketDto)
        {
            return Created("", null); // 201 Status Code
        }
    }
}
