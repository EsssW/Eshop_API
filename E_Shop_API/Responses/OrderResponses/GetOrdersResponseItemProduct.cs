namespace E_Shop_API.Responses.OrderResponses
{
    public class GetOrdersResponseItemProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string ImageURL { get; set; } = default!;
        public int Price { get; set; }
    }
}
