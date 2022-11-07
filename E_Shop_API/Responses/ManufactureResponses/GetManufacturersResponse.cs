using E_Shop_API.Responses.СategoryResponses;

namespace E_Shop_API.Responses.ManufactureResponses
{
    public class GetManufacturersResponse
    {
        public int ManufactureTotalCount { get; set; }
        public List<GetManufacturersResponseItem>? Manufacturers { get; set; }
    }
}
