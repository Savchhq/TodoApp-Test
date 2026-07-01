using TodoApp.Core.Models;

namespace TodoApp.Core.Interfaces;

public interface ITokenRepository
{
    string CreateJWTToken(AppUser user, List<string> roles);
}