using BlogPlatform.API.Services;
using Microsoft.AspNetCore.Mvc;
using BlogPlatform.Core.Dtos;
using AuthResponse = BlogPlatform.Core.Dtos.AuthResponse;

namespace BlogPlatform.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(
        [FromBody] AuthRequest request,
        [FromQuery] string displayName)
    {
        try
        {
            return _authService.RegisterAsync(request, displayName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
    {
        try
        {
            return _authService.LoginAsync(request);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}