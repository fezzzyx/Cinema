using Cinema.Application.DTOs.Movies;

namespace Cinema.Application.Interfaces.Services;

public interface IMovieService
{
    Task CreateAsync(MovieCreateDto dto);
    Task UpdateAsync(int id, MovieUpdateDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<object>> GetAllAsync();
}