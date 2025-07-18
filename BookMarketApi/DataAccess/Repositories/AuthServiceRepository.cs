using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookMarketApi.Data;
using BookMarketApi.DataAccess.Contracts;
using BookMarketApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookMarketApi.DataAccess.Repositories;

public class AuthServiceRepository : IAuthServiceRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthServiceRepository(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthResponse> Register(UserRegistration model)
    {
        if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            throw new Exception("Пользователь с таким email уже существует");

        if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            throw new Exception("Пользователь с таким именем уже существует");

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
            RegisterDate = DateTime.UtcNow,
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new AuthResponse
        {
            Token = GenerateJwtToken(user),
            Username = user.Username
        };
    }

    public async Task<AuthResponse> Login(UserLogin model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            throw new Exception("Неверный email или пароль");

        return new AuthResponse
        {
            Token = GenerateJwtToken(user),
            Username = user.Username
        };
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}