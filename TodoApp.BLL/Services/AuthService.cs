using Microsoft.AspNetCore.Identity;
using TodoApp.BLL.DTOs.Auth;
using TodoApp.BLL.Interfaces;
using TodoApp.Core.Interfaces;
using TodoApp.Core.Models;

namespace TodoApp.BLL.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> userManager;
    private readonly ITokenRepository tokenRepository;

    public AuthService(UserManager<AppUser> userManager, ITokenRepository tokenRepository)
    {
        this.userManager = userManager;
        this.tokenRepository = tokenRepository;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterRequestDto requestDto)
    {
        var user = new AppUser
        {
            UserName = requestDto.Email,
            Email = requestDto.Email
        };
        var result = await userManager.CreateAsync(user, requestDto.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRolesAsync(user, new[] { "Reader", "Writer" });
        }

        return result;
    }

    public async Task<string?> LoginAsync(LoginRequestDto requestDto)
    {
        var user = await userManager.FindByEmailAsync(requestDto.Email);

        if (user != null)
        {
            var checkPasswordResult = await userManager.CheckPasswordAsync(user, requestDto.Password);

            if (checkPasswordResult)
            {
                var roles = await userManager.GetRolesAsync(user);
                return tokenRepository.CreateJWTToken(user, roles.ToList());
            }
        }

        return null;
    }
}