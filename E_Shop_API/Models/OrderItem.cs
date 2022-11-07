using System.Drawing;

namespace E_Shop_API.Models
{
    /// <summary>
    /// Из чего состоит заказ
    /// </summary>
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }


        #region navigation Properties

        public Order? Order { get; set; }
        public Product? Product { get; set; }

        #endregion
    }
}
