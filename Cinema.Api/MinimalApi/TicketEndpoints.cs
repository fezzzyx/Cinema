using System.Security.Claims;
using Cinema.Application.DTOs.Tickets;
using Cinema.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.MinimalApi;

public static class TicketEndpoints
{
    public static void MapTicketEndpoints(this WebApplication app)
    {
        app.MapPost("/tickets",
            [Authorize]
            async (TicketCreateDto dto, HttpContext http, ITicketService service) =>
            {
                var userId = int.Parse(http.User.FindFirstValue("UserId"));
                await service.BuyAsync(userId, dto);
                return Results.Ok();
            });

        app.MapGet("/tickets/user",
            [Authorize]
            async (HttpContext http, ITicketService service) =>
            {
                var userId = int.Parse(http.User.FindFirstValue("UserId"));
                return Results.Ok(await service.GetByUserAsync(userId));
            });

        app.MapGet("/tickets",
            [Authorize(Roles = "Admin")]
            async (ITicketService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

        app.MapDelete("/tickets/{id:int}",
            [Authorize]
            async (int id, ITicketService service) =>
            {
                await service.CancelAsync(id);
                return Results.Ok();
            });
    }
}