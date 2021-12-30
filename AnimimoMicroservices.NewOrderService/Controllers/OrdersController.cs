using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimimoMicroservices.NewOrderService.Data;
using AnimimoMicroservices.NewOrderService.DTO;

namespace AnimimoMicroservices.NewOrderService.Controllers
{
    // Changed from OrderDTOController to OrdersController
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AnimimoMicroservicesNewOrderServiceContext _context;

        public OrdersController(AnimimoMicroservicesNewOrderServiceContext context)
        {
            _context = context;
        }

        // GET: api/OrderDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrderDTO()
        {
            return await _context.OrderDTO.ToListAsync();
        }

        // GET: api/OrderDTO/5
        [HttpGet("{id}")]
        public ActionResult<OrderDTO> GetOrderDTO(string id)
        {
            //var orderDTO = await _context.OrderDTO.FindAsync(id);

            var orderDTO = _context.OrderDTO
                .FirstOrDefault(x => x.Identifier == id);

            if (orderDTO == null)
            {
                return NotFound();
            }

            return orderDTO;
        }

        // PUT: api/OrderDTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDTO(string id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Identifier)
            {
                return BadRequest();
            }

            _context.Entry(orderDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDTOExists(id))
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

        // POST: api/OrderDTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrderDTO(OrderDTO orderDTO)
        {
            // TODO Kontakta basket service (GET /api/baskets/{orderDTO.Identifier})

            //  generera order (Order, OrderLine)

            _context.OrderDTO.Add(orderDTO);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderDTOExists(orderDTO.Identifier))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderDTO", new { id = orderDTO.Identifier }, orderDTO);
        }

        // DELETE: api/OrderDTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDTO(string id)
        {
            var orderDTO = await _context.OrderDTO.FindAsync(id);
            if (orderDTO == null)
            {
                return NotFound();
            }

            _context.OrderDTO.Remove(orderDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDTOExists(string id)
        {
            return _context.OrderDTO.Any(e => e.Identifier == id);
        }
    }
}
