using BlogPlatform.Core.Models;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogPlatform.Core;
using BlogPlatform.Core.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BlogPlatform.API.Services;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<AuthResponse> RegisterAsync(AuthRequest request, string displayName)
    {
        // Check if user already exists
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new Exception("User with this email already exists");
        }

        // Create new user
        var newUser = new User
        {
            Email = request.Email,
            UserName = request.Email,
            DisplayName = displayName
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // Generate JWT token
        return GenerateJwtToken(newUser);
    }

    public async Task<AuthResponse> LoginAsync(AuthRequest request)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new Exception("Invalid credentials");
        }

        // Check password
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            throw new Exception("Invalid credentials");
        }

        // Generate JWT token
        return GenerateJwtToken(user);
    }

    private AuthResponse GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("displayName", user.DisplayName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new AuthResponse(
            Token: new JwtSecurityTokenHandler().WriteToken(token),
            Email: user.Email,
            DisplayName: user.DisplayName
        );
    }
}
