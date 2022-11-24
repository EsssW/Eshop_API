using E_Shop_API.Models;
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

        

    }
}
