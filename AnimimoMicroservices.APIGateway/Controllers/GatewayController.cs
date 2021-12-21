using AnimimoMicroservices.APIGateway.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace AnimimoMicroservices.APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;
        public GatewayController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        // POST /api/Order
        [HttpPost]
        public async Task<IActionResult> RegisterOrder(OrderDTO orderDTO)
        {
            var orderJson = new StringContent(
                JsonSerializer.Serialize(orderDTO),
                Encoding.UTF8,
                Application.Json);

            var httpClient = httpClientFactory.CreateClient();

            using var httpResponseMessage =
                await httpClient.PostAsync("http://localhost:5700/api/OrderDTO", orderJson);

            return Created("", null); // 201 Created
        }

        // GET /api/Basket/{Identifier} - Get identifier key and its respective value
    }
}