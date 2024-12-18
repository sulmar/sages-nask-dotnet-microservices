using Auth.Api.Abstractions;
using Auth.Api.Infrastructure;
using Auth.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITokenService, JwtTokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("api/login", (LoginRequest request, ITokenService tokenService, HttpContext context) =>
{
if (request.Username == "john" && request.Password == "123")
{
    var identity = new UserIdentity { Username = "john", FirstName = "John", LastName = "Smith", Email = "john@domain.com" };

    var accessToken = tokenService.CreateAccessToken(identity);

        context.Response.Cookies.Append("access-token", accessToken, new CookieOptions
        {
            HttpOnly = true, // blokuje dostêp z js do document.cookie
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(15)
        });

        return Results.Ok(new { AccessToken = accessToken });
    }

    return Results.Unauthorized();
});

app.Run();

