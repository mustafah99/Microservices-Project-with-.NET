using AnimimoMicroservices.APIGateway.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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
        public async Task<IActionResult> RegisterOrder(DTO.OrderService.OrderDTO orderDTO)
        {
            // TODO Kontakta basket service (GET /api/baskets/{orderDTO.Identifier})

            //  generera order (Order, OrderLine)

            var orderJson = new StringContent(
                JsonSerializer.Serialize(orderDTO),
                Encoding.UTF8,
                Application.Json);

            var httpClient = httpClientFactory.CreateClient();

            using var httpResponseMessage =
                await httpClient.PostAsync("http://localhost:5700/api/OrderDTO", orderJson);

            return Created("", null); // 201 Created
        }


        // GET /api/Gateway/{identifier} - Get identifier key and its respective value
        [HttpGet("{identifier}")]
        public async Task<IActionResult> GetOrder(string identifier)
        {
            var orderDTO = await FetchOrder(identifier);

            if (orderDTO == null)
                return NotFound(); // 404 Not Found

            orderDTO.Basket = await FetchBasketEntries(identifier);

            return Ok(orderDTO); // 200 OK
        }
        // NEW CODE
        // POST /basket/{identifier}
        [HttpPost("basket/{identifier}")]
        public async Task<IActionResult> MakeBasketEntry(string identifier, NewBasketDTO newBasketDTO)
        {
            var postEntry = new StringContent(
                JsonSerializer.Serialize(newBasketDTO),
                Encoding.UTF8,
                Application.Json);

            var httpClient = httpClientFactory.CreateClient();

            using var httpResponseMessage =
                await httpClient.PostAsync($"https://localhost:5500/api/Basket/{identifier}", postEntry);

            if (!httpResponseMessage.IsSuccessStatusCode)
                throw new Exception();

            return Created("", null); // 201 Created
        }

        // NEW CODE

        private async Task<OrderDTO> FetchOrder(string identifier)
        {
            var httpRequestMessage = new HttpRequestMessage(
               HttpMethod.Get,
               $"http://localhost:5700/api/OrderDTO/{identifier}")
            {
                Headers = { { HeaderNames.Accept, "application/json" }, }
            };

            var httpClient = httpClientFactory.CreateClient();

            using var httpResponseMessage =
                await httpClient.SendAsync(httpRequestMessage);

            OrderDTO orderDTO = null;

            if (!httpResponseMessage.IsSuccessStatusCode)
                return orderDTO;

            using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var orderServiceDtoExtract = await JsonSerializer.DeserializeAsync
                    <DTO.OrderService.OrderDTO>(contentStream, options);

            orderDTO = new OrderDTO
            {
                Identifier = orderServiceDtoExtract.Identifier,
                Customer = orderServiceDtoExtract.Customer,
            };

            return orderDTO; // 200 OK
        }

        private async Task<IEnumerable<NewBasketDTO>> FetchBasketEntries(string identifier)
        {
            var httpRequestMessage = new HttpRequestMessage(
               HttpMethod.Get,
               $"https://localhost:5500/api/Basket/{identifier}")
            {
                Headers = { { HeaderNames.Accept, "application/json" }, }
            };

            var httpClient = httpClientFactory.CreateClient();

            using var httpResponseMessage =
                await httpClient.SendAsync(httpRequestMessage);

            var basketEntires = Enumerable.Empty<NewBasketDTO>();

            if (!httpResponseMessage.IsSuccessStatusCode)
                return basketEntires;

            using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var basketDTO = await JsonSerializer.DeserializeAsync
                    <DTO.BasketService.JournalDto>(contentStream, options);

            basketEntires = basketDTO.Entries.Select(x =>
               new NewBasketDTO
               {
                  Identifier = x.Identifier
               }
           );

            return basketEntires; // 200 OK
        }


    }
}