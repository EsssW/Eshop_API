namespace E_Shop_API.Requests.ProductRequests
{
    public class ProductGetRequest
    {
        public string? SearchQuery { get; set; }

        public List<int>? ManufactureFilters { get; set; }
        public List<int>? CategoryFilters { get; set; }
        
    }
}
