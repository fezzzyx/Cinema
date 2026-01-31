using Cinema.Application.Constants;
using Cinema.Application.DTOs.Movies;
using Cinema.Application.Interfaces.Repositories;
using Cinema.Application.Interfaces.Services;
using Cinema.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Cinema.Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepo;
    private readonly IMemoryCache _cache;

    public MovieService(IMovieRepository movieRepo, IMemoryCache cache)
    {
        _movieRepo = movieRepo;
        _cache = cache;
    }

    public async Task<IEnumerable<object>> GetAllAsync()
    {
        if (_cache.TryGetValue(CacheKeys.Movies, out IEnumerable<object> cachedMovies))
        {
            return cachedMovies;
        }

        var movies = await _movieRepo.GetAllAsync();

        var result = movies.Select(m => new
        {
            m.Id,
            m.Title,
            m.Genre,
            m.DurationMinutes,
            m.Description
        }).ToList();

        _cache.Set(
            CacheKeys.Movies,
            result,
            TimeSpan.FromMinutes(5)
        );

        return result;
    }

    public async Task CreateAsync(MovieCreateDto dto)
    {
        await _movieRepo.AddAsync(new Movie
        {
            Title = dto.Title,
            Genre = dto.Genre,
            DurationMinutes = dto.DurationMinutes,
            Description = dto.Description
        });

        _cache.Remove(CacheKeys.Movies);
    }

    public async Task UpdateAsync(int id, MovieUpdateDto dto)
    {
        var movie = await _movieRepo.GetByIdAsync(id)
                    ?? throw new Exception("Movie not found");

        movie.Title = dto.Title;
        movie.Genre = dto.Genre;
        movie.DurationMinutes = dto.DurationMinutes;
        movie.Description = dto.Description;

        await _movieRepo.UpdateAsync(movie);

        _cache.Remove(CacheKeys.Movies);
    }

    public async Task DeleteAsync(int id)
    {
        var movie = await _movieRepo.GetByIdAsync(id)
                    ?? throw new Exception("Movie not found");

        await _movieRepo.DeleteAsync(movie);

        _cache.Remove(CacheKeys.Movies);
    }
}
