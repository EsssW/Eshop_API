using E_Shop_API.Responses.ProductResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly EShopDbContext _context;

        public ProductsController(EShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<GetProductResponse> Get()
        {
            // создание запроса и внутри него "сопоставление полей"
            var query = _context.Products
                .Select(x => new GetProductResponseItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageURL = x.ImageURL,
                    Price = x.Price,
                    Manufacture = new GetProductResponseItemManufacture()
                    {
                        Id = x.Manufacture!.Id,
                        Name = x.Manufacture!.Name,
                    },
                    Category = new GetProductResponseItemCategory()
                    {
                        Id = x.Сategory!.Id,
                        Name=x.Сategory!.Name,
                    }
                });

            var productTotalCount = await query.CountAsync();
            var products = await query.ToListAsync();

            return new GetProductResponse()
            {
                ProductTotalCount = productTotalCount,
                Products = products,
            };

        }
    }
}
