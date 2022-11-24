using E_Shop_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}
