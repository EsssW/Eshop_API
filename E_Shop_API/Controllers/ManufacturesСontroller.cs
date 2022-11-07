using E_Shop_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturesСontroller : ControllerBase
    {
        private readonly EShopDbContext _context;

        public ManufacturesСontroller(EShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<string>> GET()
        {
            return await _context.Manufactures
                .Select(x => x.Name)
                .ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<List<Manufacture>>> Create(Manufacture manufacture)
        {
            _context.Manufactures.Add(manufacture);
            _context.SaveChanges();

            return await _context.Manufactures
                    .ToListAsync();
        }

    }
}
