using Cinema.Application.DTOs.Tickets;
using Cinema.Application.Interfaces.Repositories;
using Cinema.Application.Interfaces.Services;
using Cinema.Domain.Entities;
using Cinema.Domain.Enums;

namespace Cinema.Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepo;

    public TicketService(ITicketRepository ticketRepo)
    {
        _ticketRepo = ticketRepo;
    }

    public async Task BuyAsync(int userId, TicketCreateDto dto)
    {
        var ticket = new Ticket
        {
            UserId = userId,
            MovieId = dto.MovieId,
            PurchaseDate = DateTime.UtcNow,
            Status = TicketStatus.Active
        };

        await _ticketRepo.AddAsync(ticket);
    }

    public async Task<IEnumerable<object>> GetAllAsync()
    {
        var tickets = await _ticketRepo.GetAllAsync();

        return tickets.Select(t => new
        {
            t.Id,
            t.UserId,
            t.MovieId,
            t.PurchaseDate,
            t.Status
        });
    }

    public async Task<IEnumerable<object>> GetByUserAsync(int userId)
    {
        var tickets = await _ticketRepo.GetByUserAsync(userId);

        return tickets.Select(t => new
        {
            t.Id,
            t.MovieId,
            t.PurchaseDate,
            t.Status
        });
    }

    public async Task CancelAsync(int ticketId)
    {
        var ticket = await _ticketRepo.GetByIdAsync(ticketId)
                     ?? throw new Exception("Ticket not found");

        ticket.Status = TicketStatus.Cancelled;
        await _ticketRepo.UpdateAsync(ticket);
    }
}