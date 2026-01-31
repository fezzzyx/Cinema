using Cinema.Domain.Entities;

namespace Cinema.Application.Interfaces.Services;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}