namespace E_Shop_API.Requests
{
    public class GetRequest
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; }

        public string OrderBy { get; set; } = "Id";

        public bool IsAscending { get; set; } = true;
    }
}
