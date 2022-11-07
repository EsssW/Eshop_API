namespace E_Shop_API.Responses.ProductResponses
{
    public class GetProductResponseItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string ImageURL { get; set; } = default!;
        public int Price { get; set; }

        public GetProductResponseItemManufacture Manufacture { get; set; }
        public GetProductResponseItemCategory Category { get; set; }
    }
}
