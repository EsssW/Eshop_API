namespace E_Shop_API.Models
{
    /// <summary>
    /// Пользовтаель
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Phone { get; set; }
        public string? Email { get; set; }

    }
}
