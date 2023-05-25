using Assignment3API.Data;
using Assignment3API.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Assignment3API.Service
{
    public class UserService
    {
        private readonly MyDbContext _context;
        public UserService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<User> Register(string username, string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();

            var user = new User
            {
                Username = username,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("eBH6NzB7CJdr9UaYVmPv8ZgEtkFh37RU\r\n");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            // Add other claims as needed
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        public async Task<string> Login(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return null;
            }

            // verify the password
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return null;
                }
            }

            // if password is valid, generate and return a token
            // note: you will need to implement this logic based on your application's requirements
            // here's an example of what the method could look like:
            return GenerateJwtToken(user);
        }

    }
}
