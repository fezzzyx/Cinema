using Cinema.Application.Interfaces.Repositories;
using Cinema.Application.Interfaces.Services;
using Cinema.Application.Services;
using Cinema.Infrastructure.Repositories;
using Cinema.Infrastructure.Security;

namespace Cinema.Extensions;


public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<ITicketService, TicketService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();

        services.AddScoped<IJwtGenerator, JwtGenerator>();

        return services;
    }
}