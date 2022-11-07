using System.Drawing;

namespace E_Shop_API.Models
{
    /// <summary>
	/// Продукт
	/// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageURL { get; set; } = null!;

        public int Price { get; set; }

        public int? ManufactureId { get; set; }
        public int? СategoryId { get; set; }


        #region navigation Properties

        public virtual Manufacture? Manufacture { get; set; }
        public virtual Сategory? Сategory { get; set; }
        public virtual List<Order>? Orders { get; set; }

        public List<OrderItem>? OrderItems { get; set; }

        #endregion


    }
}
