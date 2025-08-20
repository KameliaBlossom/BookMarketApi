using BookMarketApi.Common.Entities.DTOs.AuthDTOs;
using BookMarketApi.Common.Entities.InputModels.UserInputModels;

namespace BookMarketApi.BLL.Contracts.AuthContracts;

public interface IAuthContract
{
    Task<AuthResponseDto> Register(UserRegistrationModel model);
    Task<AuthResponseDto> Login(UserLoginModel model);
}