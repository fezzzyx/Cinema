namespace Cinema.Domain.Entities;


public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Genre { get; set; }
    public int DurationMinutes { get; set; }
    public string Description { get; set; }

    public ICollection<Ticket> Tickets { get; set; }
}