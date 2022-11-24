namespace E_Shop_API.Responses.OrderResponses
{
    public class GetOrdersResponse
    {
        public int OrderTotalCount { get; set; }
        public List<GetOrdersResponseItem>? Items { get; set; }
    }
}
