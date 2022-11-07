using E_Shop_API.Responses.ManufactureResponses;
using E_Shop_API.Responses.СategoryResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly EShopDbContext _context;

        public ManufacturersController(EShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<GetManufacturersResponse> Get()
        {
            var query = _context.Manufactures
                .Select(x => new GetManufacturersResponseItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                });
            var manufactureTotalCount = await query.CountAsync();
            var manufacturers = await query
                .OrderBy(x => x.Name)
                .ToListAsync();

            return new GetManufacturersResponse()
            {
                ManufactureTotalCount = manufactureTotalCount,
                Manufacturers= manufacturers
            };
        }
    }
}
