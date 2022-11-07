using E_Shop_API.Models;
using E_Shop_API.Responses.СategoryResponses;
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
        public async Task<GetCategorysResponse> Get()
        {
            var query = _context.Сategorys
                .Select(x => new GetCategorysResponseItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                });
            var categoryTotalCount = await query.CountAsync();
            var categorys = await query
                .OrderBy(x => x.Name)
                .ToListAsync();

            return new GetCategorysResponse()
            {
                CategoryTotalCount = categoryTotalCount,
                Categorys = categorys
            };
        }
    }
}
