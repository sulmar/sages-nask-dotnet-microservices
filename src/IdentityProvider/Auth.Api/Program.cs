using Auth.Api.Abstractions;
using Auth.Api.Infrastructure;
using Auth.Api.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITokenService, JwtTokenService>();
builder.Services.AddTransient<IPasswordHasher<LoginRequest>, PasswordHasher<LoginRequest>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("api/login", (LoginRequest request, ITokenService tokenService,
    HttpContext context,
    IPasswordHasher<LoginRequest> passwordHasher) =>
{
    string hashedPassword = passwordHasher.HashPassword(request, "123");

    var result = passwordHasher.VerifyHashedPassword(request, hashedPassword, request.Password);

    if (request.Username == "john" && result == PasswordVerificationResult.Success)
    {
        var identity = new UserIdentity { Username = "john", FirstName = "John", LastName = "Smith", Email = "john@domain.com", Birthday = DateTime.Parse("2000-12-01") };

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

