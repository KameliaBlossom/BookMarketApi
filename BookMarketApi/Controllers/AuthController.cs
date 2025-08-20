using BookMarketApi.BLL.Contracts.AuthContracts;
using BookMarketApi.Common.Entities.DTOs.AuthDTOs;
using BookMarketApi.Common.Entities.InputModels.UserInputModels;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthContract _authService;

    public AuthController(IAuthContract authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(UserRegistrationModel model)
    {
        try
        {
            var result = await _authService.Register(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(UserLoginModel model)
    {
        try
        {
            var result = await _authService.Login(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
