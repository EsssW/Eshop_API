using E_Shop_API.Models;
using E_Shop_API.Requests.UserRequests;
using E_Shop_API.Responses._UserResponses;
using E_Shop_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace E_Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EShopDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(EShopDbContext context, IConfiguration configuration, IUserService userService)
        {
            _context = context;
            _configuration= configuration;
            _userService = userService;
        }
        [HttpGet]
        [Authorize]
        public async Task<UserResponses> Get()
        {
            var login = _userService.GetLogin();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == login)
                ?? throw new Exception("User not found");

            return new UserResponses()
            {
                Login = login,
                Name = user.Name,
                Phone = user.Phone,
                Email = user.Email
            };
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterRequest request)
        {
            var isLoginUnique = _context.Users.All(x => x.Login != request.Login);
            if (!isLoginUnique)
                throw new Exception("User with such login already existis");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User();

            user.Login = request.Login;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Name = request.Name;
            user.Email = request.Email;
            user.Phone = request.Phone;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user.Id);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == request.Login)
                ?? throw new Exception("User not found");

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        [HttpPut]
        [Authorize]
        public async Task Put(UserPutRequest request)
        {
            var login = _userService.GetLogin();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == login)
                ?? throw new Exception("User not found");

            if (request.Name != null)
                user.Name = request.Name;
            if (request.Phone != null)
                user.Phone = request.Phone;
            if (request.Email != null)
                user.Email = request.Email;

            await _context.SaveChangesAsync();
        }

        [HttpDelete]
        [Authorize]
        public async Task Delete()
        {
            var login = _userService.GetLogin();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == login)
                ?? throw new Exception("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        #region Creae password hash and verify passhash

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        #endregion
    }
}
