using E_Shop_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace E_Shop_API
{
    public class EShopDbContext : DbContext 
    {
        public EShopDbContext()
        {
        }

        public EShopDbContext(DbContextOptions<EShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Manufacture> Manufactures { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Сategory> Сategorys { get; set; } = null!;
    }
}
