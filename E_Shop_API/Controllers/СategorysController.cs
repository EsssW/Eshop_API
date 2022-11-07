using E_Shop_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class СategorysController : ControllerBase
    {
        private readonly EShopDbContext _context;

        public СategorysController(EShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GET(int categoryId)
        {
            return await _context.Products
                .Where(x => x.СategoryId == categoryId)
                .ToListAsync();
        }
    }
}
