using Cinema.Application.DTOs.Auth;

namespace Cinema.Application.Interfaces.Services;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
}