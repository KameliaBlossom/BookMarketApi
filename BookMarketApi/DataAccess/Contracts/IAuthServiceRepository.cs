using BookMarketApi.Model;

namespace BookMarketApi.DataAccess.Contracts;

public interface IAuthServiceRepository
{
    Task<AuthResponse> Register(UserRegistration model);
    Task<AuthResponse> Login(UserLogin model);
}