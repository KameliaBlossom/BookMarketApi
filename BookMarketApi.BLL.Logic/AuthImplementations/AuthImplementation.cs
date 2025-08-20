using System.Security.Claims;
using BookMarketApi.Common.Entities.Domain.UserEntities;
using BookMarketApi.DAL.Contracts.AuthContracts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BookMarketApi.BLL.Contracts.AuthContracts;
using BookMarketApi.Common.Entities.DTOs.AuthDTOs;
using BookMarketApi.Common.Entities.InputModels.UserInputModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;


namespace BookMarketApi.BLL.Logic.AuthImplementations;

    public class AuthImplementation : IAuthContract
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthImplementation(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> Register(UserRegistrationModel model)
        {
            if (await _authRepository.UserExistsByEmailAsync(model.Email))
                throw new Exception("Пользователь с таким email уже существует");

            if (await _authRepository.UserExistsByUsernameAsync(model.Username))
                throw new Exception("Пользователь с таким именем уже существует");

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                RegisterDate = DateTime.UtcNow,
                IsActive = true
            };

            await _authRepository.AddUserAsync(user);

            return new AuthResponseDto()
            {
                Token = GenerateJwtToken(user),
                Username = user.Username
            };
        }

        public async Task<AuthResponseDto> Login(UserLoginModel model)
        {
            var user = await _authRepository.GetUserByEmailAsync(model.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new Exception("Неверный email или пароль");

            return new AuthResponseDto()
            {
                Token = GenerateJwtToken(user),
                Username = user.Username
            };
        }

        private string GenerateJwtToken(User user)
        {
            var key = _configuration["Jwt:Key"];
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
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