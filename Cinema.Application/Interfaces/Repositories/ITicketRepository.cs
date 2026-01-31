using Cinema.Domain.Entities;

namespace Cinema.Application.Interfaces.Repositories;


public interface ITicketRepository
{
    Task<List<Ticket>> GetAllAsync();
    Task<List<Ticket>> GetByUserAsync(int userId);
    Task<Ticket?> GetByIdAsync(int id);
    Task AddAsync(Ticket ticket);
    Task UpdateAsync(Ticket ticket);
}