using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ProductCatalog.Api.Authorization;
using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IProductRepository, FakeProductRepository>();
builder.Services.AddSingleton<Context>(sp =>
{
    List<Product> products = [
        new Product(1, "Popular Product", 80.00m),
        new Product(2, "Special Item", 80.00m, 50m),
        new Product(3, "Extra Product", 80.00m),
        new Product(4, "Bonus Product", 80.00m, 70m),
        new Product(5, "Fancy Product", 80.00m, 70m),
        new Product(6, "Smart Product", 99.99m),
        new Product(7, "Old-school Product", 199.00m),
        new Product(8, "Future Product", 1.00m)
    ];

    return new Context { Products = products.ToDictionary(p => p.Id) };
});


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    policy.WithOrigins("https://localhost:7108");
    policy.WithMethods("GET");
    policy.AllowAnyMethod();
}));


string privateKey = "your_secret_key_your_secret_key_your_secret_key";

builder.Services.AddFastEndpoints();
builder.Services.AddAuthenticationJwtBearer(options => options.SigningKey = privateKey);
builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("gold", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(JwtRegisteredClaimNames.Birthdate);
        policy.RequireClaim(JwtRegisteredClaimNames.Email);
        policy.Requirements.Add(new AgeAuthorizationRequirement(18));        
    });


builder.Services.AddTransient<IAuthorizationHandler, AdultAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, ProductOwnerAuthorizationHandler>();

builder.Services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

// app.MapGet("api/products", async (IProductRepository repository) => await repository.GetAllAsync());
// app.MapGet("api/products/{id:int}", async(int id, IProductRepository repository) => await repository.GetAsync(id));


app.MapFastEndpoints();

app.Run();

