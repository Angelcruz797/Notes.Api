using Microsoft.AspNetCore.Mvc;
using Notes.Api.Dto;
using Notes.Api.Repository;
using Notes.Api.Services;

namespace Notes.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest register)
    {
        if (string.IsNullOrWhiteSpace(register.UserName) ||
            string.IsNullOrWhiteSpace(register.Email) ||
            string.IsNullOrWhiteSpace(register.Password))
        {
            return BadRequest(new { message = "UserName, Email and Password must not be empty" });
        }

        if (await service.UserExistsAsync(register.UserName, register.Email))
        {
            return Conflict(new { message = "UserName or Email is already taken" });
        }

        return Ok(await service.RegisterAsync(register));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest login)
    {
        if (string.IsNullOrWhiteSpace(login.UserName) || string.IsNullOrWhiteSpace(login.Password))
        {
            return BadRequest(new { message = "UserName and Password must not be empty" });
        }

        var result = await service.LoginAsync(login);
        if (result is null)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }

        return Ok(result);
    }
}