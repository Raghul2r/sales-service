using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAndService.Models;

namespace SalesAndService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnalysisController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("revenue")]
        public IActionResult GetRevenue(DateTime start, DateTime end)
        {
            var revenue = _context.Orders.Where(o => o.DateOfSale >= start && o.DateOfSale <= end)
                                         .Sum(o => o.QuantitySold * o.UnitPrice * (1 - o.Discount));

            return Ok(new { totalRevenue = revenue });
        }

        [HttpGet("revenue-by-category")]
        public IActionResult GetRevenueByCategory(DateTime start, DateTime end)
        {
            var data = _context.Orders.Include(o => o.Product)
                                      .Where(o => o.DateOfSale >= start && o.DateOfSale <= end)
                                      .GroupBy(o => o.Product.Category)
                                      .Select(g => new {
                                                         Category = g.Key,
                                                         Revenue = g.Sum(o => o.QuantitySold * o.UnitPrice * (1 - o.Discount))
                                      }).ToList();

            return Ok(data);
        }

        [HttpPost("add-order")]
        public async Task<IActionResult> AddOrder([FromBody] Order dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = new Order
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                CustomerId = dto.CustomerId,
                Region = dto.Region,
                DateOfSale = dto.DateOfSale,
                QuantitySold = dto.QuantitySold,
                UnitPrice = dto.UnitPrice,
                Discount = dto.Discount,
                ShippingCost = dto.ShippingCost,
                PaymentMethod = dto.PaymentMethod
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order added successfully", order.Id });
        }

    }
}
