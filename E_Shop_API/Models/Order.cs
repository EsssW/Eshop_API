namespace E_Shop_API.Models
{
    /// <summary>
    /// Заказ 
    /// </summary>
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDateTime { get; set; }

        public string Addres { get; set; } = null!;

        public int ProductCount { get; set; }

        public int Sum { get; set; }


        #region navigation Properties

        public User? User { get; set; }

        public List<Product>? Product { get; set; }
        public List<OrderItem>? OrderItems { get; set; }

        #endregion
    }
}
