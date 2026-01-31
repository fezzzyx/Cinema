using Cinema.Domain.Enums;

namespace Cinema.Domain.Entities;

public class Ticket
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public int MovieId { get; set; }

    public DateTime PurchaseDate { get; set; }
    public TicketStatus Status { get; set; }

    public User User { get; set; }
    public Movie Movie { get; set; }
}