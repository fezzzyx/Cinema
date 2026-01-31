using Cinema.Domain.Entities;

namespace Cinema.Application.Interfaces.Repositories;


public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
}