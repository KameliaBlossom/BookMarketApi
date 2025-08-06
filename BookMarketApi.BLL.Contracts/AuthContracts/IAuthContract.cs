using BookMarketApi.Common.Entities.DTOs.AuthDTOs;

namespace BookMarketApi.BLL.Contracts.AuthContracts;

public interface IAuthContract
{
    Task<AuthResponseDto> Register(UserRegistrationDto model);
    Task<AuthResponseDto> Login(UserLoginDto model);
}