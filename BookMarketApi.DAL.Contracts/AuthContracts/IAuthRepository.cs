using BookMarketApi.Common.Entities.Domain.UserEntities;

namespace BookMarketApi.DAL.Contracts.AuthContracts;

public interface IAuthRepository
{
    Task<bool> UserExistsByEmailAsync(string email);
    Task<bool> UserExistsByUsernameAsync(string username);
    Task<User> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
}