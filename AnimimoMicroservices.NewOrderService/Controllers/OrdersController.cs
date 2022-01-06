using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimimoMicroservices.NewOrderService.Data;
using AnimimoMicroservices.NewOrderService.Models.Domain;
using AnimimoMicroservices.NewOrderService.Models.DTO;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using AnimimoMicroservices.NewOrderService.DTO;

namespace AnimimoMicroservices.NewOrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AnimimoMicroservicesNewOrderServiceContext _context;
        private readonly IHttpClientFactory httpClientFactory;

        public OrdersController(IHttpClientFactory httpClientFactory, AnimimoMicroservicesNewOrderServiceContext context)
        {
            _context = context;
            this.httpClientFactory = httpClientFactory;
        }

        // GET: https://localhost:5500/api/Basket/{identifier}
        [HttpGet("/basket/{identifier}")]
        public async Task<BasketDto> GetBasketItems(string identifier)
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

            BasketDto items = null;

            if (!httpResponseMessage.IsSuccessStatusCode)
                return items;

            using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var basketDto = await System.Text.Json.JsonSerializer.DeserializeAsync
                    <BasketDto>(contentStream, options);

            //items = basketDto.Items.Select(x =>
            //    new BasketEntryDto
            //    {
            //        ProductId = x.ProductId,
            //        Quantity = x.Quantity,
            //    }
            //);

            items = new BasketDto
            {
                Identifier = basketDto.Identifier,
                Items = basketDto.Items,
            };

            //var entryJson = new StringContent(
            //    JsonSerializer.Serialize(items),
            //    Encoding.UTF8,
            //    Application.Json);

            //await httpClient.PostAsync($"http://localhost:5700/api/Orders", entryJson);

            return items; // 200 OK
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Order.ToListAsync();
        }

        // GET: api/OrderLine
        [HttpGet("/api/OrderLine")]
        public async Task<ActionResult<IEnumerable<OrderLine>>> GetOrderLine()
        {
            return await _context.OrderLine.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(PostOrderDTO postOrderDto)
        {
            //var httpClient = httpClientFactory.CreateClient();

            var order = new Order(postOrderDto.Identifier, postOrderDto.Customer);

            var json = JsonConvert.SerializeObject(order);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //await httpClient.PostAsync($"http://localhost:5700/basket/{order.Identifier}", content);

            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            // kod här
            var basket = await GetBasketItems(postOrderDto.Identifier);
            var newOrderId = order.OrderID;

            foreach (var item in basket.Items)
            {
                var orderLine = new OrderLine
                {
                    OrderID = newOrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                _context.OrderLine.Add(orderLine);
            };

            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
            //return CreatedAtAction("GetOrder", new { id = order.OrderID }, newOrderId);

            return Created("", new { orderId = order.OrderID });

            //return Ok(new { orderId = order.OrderID });
        }

        // POST: api/OrderLine
        [HttpPost("/api/OrderLine")]
        public async Task<ActionResult<Order>> PostOrderLine(OrderLine orderLine)
        {
            _context.OrderLine.Add(orderLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = orderLine.Id }, orderLine);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}
