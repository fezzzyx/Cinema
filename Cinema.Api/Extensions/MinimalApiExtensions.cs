using Cinema.MinimalApi;

namespace Cinema.Extensions;

public static class MinimalApiExtensions
{
    public static WebApplication MapMinimalApi(this WebApplication app)
    {
        app.MapAuthEndpoints();
        app.MapMovieEndpoints();
        app.MapTicketEndpoints();

        return app;
    }
}