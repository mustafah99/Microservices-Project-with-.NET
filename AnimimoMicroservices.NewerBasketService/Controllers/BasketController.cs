using AnimimoMicroservices.NewerBasketService.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

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

        [HttpPut("{Identifier}")]
        public IActionResult UpdateBasket(string Identifier, NewBasketDTO newBasketDTO)
        {
            var getBasketIdentifier = Cache.GetString(Identifier);


            if (getBasketIdentifier == null)
            {
                var putSerializedData = JsonSerializer.Serialize(newBasketDTO);

                Cache.SetString(newBasketDTO.Identifier, putSerializedData);

                return Created("", null);

            }
            else
            {
                var putNewSerializedData = JsonSerializer.Serialize(newBasketDTO);

                Cache.SetStringAsync(newBasketDTO.Identifier, putNewSerializedData);
            }

            return Created("", null); // Returns 201 Status Code
        }


        [HttpGet("{Identifier}")]
        public ActionResult<NewBasketDTO> GetBasket(string Identifier)
        {
            var serializedBasket = Cache.GetString(Identifier);

            if (serializedBasket == null)
                return NotFound(); // 404

            var basketDto = JsonSerializer.Deserialize<NewBasketDTO>(serializedBasket);

            return Ok(basketDto);
        }
    }
}
