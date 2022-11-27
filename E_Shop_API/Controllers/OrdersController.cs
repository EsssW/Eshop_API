using E_Shop_API.Models;
using E_Shop_API.Requests.OrderRequests;
using E_Shop_API.Responses.OrderResponses;
using E_Shop_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly EShopDbContext _context;
        private readonly IUserService _userService;

        public OrdersController(
            EShopDbContext context,
            IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<GetOrdersResponse> Get()
        {
            var login = _userService.GetLogin();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == login)
                ?? throw new Exception("User not found");

            var query = _context.Orders
                .Where(x => x.User == user)
                .Select(x => new GetOrdersResponseItem()
                {
                    Id = x.Id,
                    OrderDateTime = x.OrderDateTime,
                    Addres = x.Addres,
                    Sum = x.Sum,
                    ProductCount = x.ProductCount,
                    OrderItems = x.OrderItems!.Select(i => new GetOrdersResponseItemOrderItem()
                    {
                        Id = i.Id,
                        Product = new GetOrdersResponseItemProduct()
                        {
                            Id = i.Product!.Id,
                            ImageURL = i.Product!.ImageURL,
                            Name = i.Product!.Name,
                            Price = i.Product!.Price,
                        }
                    })
                    .ToList()
                });

            var orders = await query.ToListAsync();
            var count = await query.CountAsync();

            return new GetOrdersResponse()
            {
                Items = orders,
                OrderTotalCount = count,
            };
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> Post(OrderPostRequest request)
        {
            var login = _userService.GetLogin();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == login)
                ?? throw new Exception("User not found");

            if (!isOrderItemsUnique(request))
                throw new Exception("Product must be unique");

            var products = await _context.Products
                .Where(x => request.OrderItems.Select(x => x.ProductId).Contains(x.Id))
                .ToListAsync();

            var orderItems = request.OrderItems
                .Select(x => new OrderItem()
                {
                    Product = products.FirstOrDefault(p => p.Id == x.ProductId)
                        ?? throw new Exception($"Not found shoe with id {x.ProductId}")
                })
                .ToList();

            var order = new Order()
            {
                OrderDateTime = DateTime.UtcNow,
                Addres = request.Addres,
                ProductCount = orderItems.Count(),
                Sum = orderItems.Sum(x => x.Product!.Price),
                User = user,
                OrderItems = orderItems,
            };

            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            return Ok(order.Id);
        }

        private bool isOrderItemsUnique(OrderPostRequest request)
        {
            return request.OrderItems
                .GroupBy(x => new { x.ProductId })
                .All(x => x.Count() == 1);
        }

    }
}
