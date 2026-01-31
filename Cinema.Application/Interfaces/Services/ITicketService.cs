using Cinema.Application.DTOs.Tickets;

namespace Cinema.Application.Interfaces.Services;

public interface ITicketService
{
    Task BuyAsync(int userId, TicketCreateDto dto);
    Task<IEnumerable<object>> GetAllAsync();
    Task<IEnumerable<object>> GetByUserAsync(int userId);
    Task CancelAsync(int ticketId);
}