namespace E_Shop_API.Requests.UserRequests
{
    public class RegisterRequest
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;

        public string Name { get; set; } = null!;

        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
