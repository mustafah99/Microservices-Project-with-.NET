//using AnimimoMicroservices.NewOrderService.Data;
//using AnimimoMicroservices.NewOrderService.DTO;
//using AnimimoMicroservices.NewOrderService.Models.DTO;
//using AnimimoMicroservices.NewOrderService.Models.Domain;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Net.Http.Headers;
//using Newtonsoft.Json;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using static System.Net.Mime.MediaTypeNames;
//using JsonSerializer = System.Text.Json.JsonSerializer;

//namespace AnimimoMicroservices.NewOrderService.Controllers
//{
//    // Changed from OrderDTOController to OrdersController
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrdersController : ControllerBase
//    {
//        private readonly IHttpClientFactory httpClientFactory;
//        private readonly AnimimoMicroservicesNewOrderServiceContext _context;


//        public OrdersController(IHttpClientFactory httpClientFactory, AnimimoMicroservicesNewOrderServiceContext context)
//        {
//            _context = context;
//            this.httpClientFactory = httpClientFactory;
//        }


//        //// I want to use this constructor as well as 
//        //[ActivatorUtilitiesConstructor]
//        //public OrdersController(IHttpClientFactory httpClientFactory)
//        //{
//        //    this.httpClientFactory = httpClientFactory;
//        //}

//        //// this one
//        //public OrdersController(AnimimoMicroservicesNewOrderServiceContext context)
//        //{
//        //    _context = context;
//        //}

//        // GET: api/Order
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<DTO.Order>>> GetOrderDTO()
//        {
//            return await _context.Order.ToListAsync();
//        }

//        [HttpGet("/new")]
//        public async Task<ActionResult<IEnumerable<OrderLine>>> getOrderLine()
//        {
//            return await _context.OrderLine.ToListAsync();
//        }

//        // GET: api/Order/5
//        [HttpGet("{id}")]
//        public ActionResult<DTO.Order> GetOrderDTO(string id)
//        {
//            //var orderDTO = await _context.Order.FindAsync(id);

//            var orderDTO = _context.OrderDTO
//                .FirstOrDefault(x => x.Identifier == id);

//            if (orderDTO == null)
//            {
//                return NotFound();
//            }

//            return orderDTO;
//        }

//        // PUT: api/Order/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutOrderDTO(string id, DTO.Order orderDTO)
//        {
//            if (id != orderDTO.Identifier)
//            {
//                return BadRequest();
//            }

//            _context.Entry(orderDTO).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!OrderDTOExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // FETCH FROM BASKET BY IDENTIFIER TO POST TO ORDER
//        [HttpGet("basket/{identifier}")]
//        public async Task<IEnumerable<BasketDto>> FetchBasketEntries(string identifier)
//        {
//            var httpRequestMessage = new HttpRequestMessage(
//               HttpMethod.Get,
//               $"https://localhost:5500/api/Basket/{identifier}")
//            {
//                Headers = { { HeaderNames.Accept, "application/json" }, }
//            };

//            var httpClient = httpClientFactory.CreateClient();

//            using var httpResponseMessage =
//                await httpClient.SendAsync(httpRequestMessage);

//            var basketEntries = Enumerable.Empty<BasketEntryDto>();

//            if (!httpResponseMessage.IsSuccessStatusCode)
//                return basketEntries;

//            using var contentStream =
//                await httpResponseMessage.Content.ReadAsStreamAsync();

//            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

//            var basketDTO = await JsonSerializer.DeserializeAsync
//                    <BasketDto>(contentStream, options);

//            //basketDTO = new NewBasketDTO.ItemDTO
//            //{
//            //    ProductId = basketDTO.ProductId,
//            //    Quantity = basketDTO.Quantity
//            //};

//            basketEntries = basketDTO.Items.Select(x =>
//                new BasketEntryDto
//                {
//                    ProductId = x.ProductId,
//                    Quantity = x.Quantity
//                }
//            );

//            return basketEntries; // 200 OK
//        }

//        // FETCH FROM BASKET

//        // SLÅ IHOP BASKET OCH ORDER
//        [HttpPost("basket/together/{identifier}")]
//        public async Task<IActionResult> GetOrderAndBasket(string identifier)
//        {
//            // Aggregera data från två servicar, och returnerar patient 
//            // samt dennas journal.

//            var patientDto = await FetchOrder(identifier);

//            if (patientDto == null)
//                return NotFound(); // 404 Not Found

//            patientDto.Items = await FetchBasketEntries(identifier);

//            var journalEntryJson = new StringContent(
//                JsonSerializer.Serialize(patientDto),
//                Encoding.UTF8,
//                Application.Json);

//            var httpClient = httpClientFactory.CreateClient();

//            using var httpResponseMessage =
//                await httpClient.PostAsync($"http://localhost:5700/new/post", journalEntryJson);

//            return Ok(patientDto); // 200 OK
//        }
//        // OVANFÖR

//        [HttpGet("basket/together/{identifier}")]
//        public async Task<IActionResult> GetOrderBasket(string identifier)
//        {
//            // Aggregera data från två servicar, och returnerar patient 
//            // samt dennas journal.

//            var patientDto = await FetchOrder(identifier);

//            if (patientDto == null)
//                return NotFound(); // 404 Not Found

//            patientDto.Items = await FetchBasketEntries(identifier);

//            return Ok(patientDto); // 200 OK
//        }

//        // POST: api/Order
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<DTO.Order>> PostOrderDTO(DTO.Order orderDTO)
//        {
//            // TODO Contact BasketService (GET /api/Baskets/{identifier} and take out 'items array with objects inside' and post to Order

//            //var httpClient = new HttpClient();
//            // FETCHING BASKET
//            //var items = await FetchBasketEntries(orderDTO.Identifier);
//            var httpClient = httpClientFactory.CreateClient();

//            // Get Basket and Order by ID

//            // UP HERE
//            var json = JsonConvert.SerializeObject(orderDTO);
//            // Want to convert fetched basket to Json
//            //var jss = JsonConvert.SerializeObject(items);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");
//            //var content1 = new StringContent(jss, Encoding.UTF8, "application/json");

//            //var patientDto = await GetOrderAndBasket(orderDTO.Identifier);

//            //Generate Order(Order, OrderLine)

//            // I want to send the fetched basket here with my orderDTO
//            _context.OrderDTO.Add(orderDTO);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (OrderDTOExists(orderDTO.Identifier))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            //await httpClient.PutAsync($"https://localhost:5500/api/Basket/{orderDTO.Identifier}", content);

//            await httpClient.PostAsync($"http://localhost:5700/api/Orders/basket/together/{orderDTO.Identifier}", content);

//            return CreatedAtAction("GetOrderDTO", new { id = orderDTO.Identifier }, orderDTO);
//        }

//        // ORDERLINE POST
//        [HttpPost("/new/post")]
//        public async Task<ActionResult<OrderLine>> PostOrderLine(OrderLine orderLine)
//        {
//            var httpClient = new HttpClient();
            
//            var json = JsonConvert.SerializeObject(orderLine);

//            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
//            _context.OrderLine.Add(orderLine);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (OrderDTOExists(orderLine.Identifier))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            //await httpClient.PutAsync($"https://localhost:5500/api/Basket/{orderDTO.Identifier}", content);

//            return CreatedAtAction("GetOrderDTO", new { id = orderLine.Identifier }, orderLine);
//        }
//        // ORDERLINE POST

//        // GET ORDER
//        private async Task<DTO.Order> FetchOrder(string identifier)
//        {
//            var httpRequestMessage = new HttpRequestMessage(
//               HttpMethod.Get,
//               $"http://localhost:5700/api/Orders/{identifier}")
//            {
//                Headers = { { HeaderNames.Accept, "application/json" }, }
//            };

//            var httpClient = httpClientFactory.CreateClient();

//            using var httpResponseMessage =
//                await httpClient.SendAsync(httpRequestMessage);

//            DTO.Order orderDTO = null;

//            if (!httpResponseMessage.IsSuccessStatusCode)
//                return orderDTO;

//            using var contentStream =
//                await httpResponseMessage.Content.ReadAsStreamAsync();

//            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

//            var patientServicePatientDto = await JsonSerializer.DeserializeAsync
//                    <DTO.Order>(contentStream, options);

//            orderDTO = new OrderDTO(patientServicePatientDto.Identifier)
//            {
//                OrderID = patientServicePatientDto.OrderID,
//                Identifier = patientServicePatientDto.Identifier,
//                Customer = patientServicePatientDto.Customer
//            };

//            return orderDTO; // 200 OK
//        }
//        // GET ORDER

//        // DELETE: api/Order/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteOrderDTO(string id)
//        {
//            var orderDTO = await _context.OrderDTO.FindAsync(id);
//            if (orderDTO == null)
//            {
//                return NotFound();
//            }

//            _context.OrderDTO.Remove(orderDTO);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool OrderDTOExists(string id)
//        {
//            return _context.OrderDTO.Any(e => e.Identifier == id);
//        }

//    }
//}
