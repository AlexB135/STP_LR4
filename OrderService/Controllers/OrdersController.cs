using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using Newtonsoft.Json;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> Orders = new();

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return Ok(Orders);
        }

        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            order.Id = Orders.Count > 0 ? Orders.Max(o => o.Id) + 1 : 1;
            Orders.Add(order);
            return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, Order updatedOrder)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            order.UserId = updatedOrder.UserId;
            order.ProductName = updatedOrder.ProductName;
            order.Price = updatedOrder.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            Orders.Remove(order);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<object>> GetUser(int userId)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"http://localhost:5000/api/users/{userId}"); // Укажите корректный URL UserService
            var user = JsonConvert.DeserializeObject<object>(response);

            return Ok(user);
        }
    }
}
