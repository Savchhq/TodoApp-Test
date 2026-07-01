using Microsoft.AspNetCore.Identity;
using TodoApp.BLL.DTOs.Auth;

namespace TodoApp.BLL.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterRequestDto requestDto);
    Task<string?> LoginAsync(LoginRequestDto requestDto);
}