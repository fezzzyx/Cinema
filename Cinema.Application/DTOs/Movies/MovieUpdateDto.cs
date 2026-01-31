namespace Cinema.Application.DTOs.Movies;

public class MovieUpdateDto
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int DurationMinutes { get; set; }
    public string Description { get; set; }
}