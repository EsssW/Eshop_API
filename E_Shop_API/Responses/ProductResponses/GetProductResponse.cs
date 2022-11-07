namespace E_Shop_API.Responses.ProductResponses
{
    public class GetProductResponse
    {
        public int ProductTotalCount { get; set; }
        public List<GetProductResponseItem>? Products { get;set; }
    }
}
