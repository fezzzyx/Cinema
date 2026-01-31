using Cinema.Domain.Entities;
using Cinema.Domain.Enums;
using Cinema.Extensions;
using Cinema.Filters;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.HostedServices;
using Cinema.Infrastructure.Logging;
using Cinema.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

//HostedService
builder.Services.AddSingleton<FileHealthLogger>();
builder.Services.AddHostedService<ApiHealthHostedService>();

// Cashing
builder.Services.AddMemoryCache();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
    );
});
// DI
builder.Services.AddApplicationServices();

// JWT
builder.Services.AddJwt(builder.Configuration);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapMinimalApi();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Database.Migrate();
    
    if (!context.Users.Any(u => u.Email == "admin@cinema.com"))
    {
        var admin = new User
        {
            Name = "Admin",
            Email = "admin@cinema.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Role = Role.Admin
        };

        context.Users.Add(admin);
        context.SaveChanges();
    }
}

app.Run();