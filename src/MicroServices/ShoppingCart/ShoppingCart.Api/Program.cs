using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IShoppingCartRepository, RedisShoppingCartRepository>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost:6379"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/api/cart", async (CartItem item, IShoppingCartRepository repository) =>
{
    await repository.Add(item);

});

app.MapDelete("/api/cart", async ([FromQuery] string ProductName, IShoppingCartRepository repository) =>
{
    await repository.Remove(new CartItem(ProductName));
});

app.Run();

