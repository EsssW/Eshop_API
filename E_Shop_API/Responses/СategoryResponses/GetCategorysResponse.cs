namespace E_Shop_API.Responses.СategoryResponses
{
    public class GetCategorysResponse
    {
        public int CategoryTotalCount { get; set; }
        public List<GetCategorysResponseItem> Categorys { get; set; }
    }
}
