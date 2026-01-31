using Cinema.Application.DTOs.Auth;
using Cinema.Application.Interfaces.Services;

namespace Cinema.MinimalApi;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/register", async (
            RegisterDto dto,
            IAuthService authService) =>
        {
            await authService.RegisterAsync(dto);
            return Results.Ok();
        });

        app.MapPost("/auth/login", async (
            LoginDto dto,
            IAuthService authService) =>
        {
            var token = await authService.LoginAsync(dto);
            return Results.Ok(token);
        });
    }
}