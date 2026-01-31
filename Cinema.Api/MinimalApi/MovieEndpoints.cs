using Cinema.Application.DTOs.Movies;
using Cinema.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.MinimalApi;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints(this WebApplication app)
    {
        app.MapGet("/movies", async (IMovieService service) =>
        {
            return Results.Ok(await service.GetAllAsync());
        });

        app.MapPost("/movies",
            [Authorize(Roles = "Admin")]
            async (MovieCreateDto dto, IMovieService service) =>
            {
                await service.CreateAsync(dto);
                return Results.Ok();
            });

        app.MapPut("/movies/{id:int}",
            [Authorize(Roles = "Admin")]
            async (int id, MovieUpdateDto dto, IMovieService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.Ok();
            });

        app.MapDelete("/movies/{id:int}",
            [Authorize(Roles = "Admin")]
            async (int id, IMovieService service) =>
            {
                await service.DeleteAsync(id);
                return Results.Ok();
            });
    }
}