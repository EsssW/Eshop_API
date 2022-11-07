namespace E_Shop_API.Models
{
    /// <summary>
    ///  Категории товаров 
    /// </summary>
    public class Сategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        #region navigation Properties

        public List<Product>? Products { get; set; }

        #endregion
    }
}
