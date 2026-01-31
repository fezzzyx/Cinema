namespace Cinema.Application.DTOs.Tickets;

public class TicketDto
{
    public int Id { get; set; }
    public string MovieTitle { get; set; }
    public string Status { get; set; }
    public DateTime PurchaseDate { get; set; }
    
    public string UserName { get; set; }
    public string UserEmail { get; set; }
}