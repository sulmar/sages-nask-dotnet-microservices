using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Entities;
using StackExchange.Redis;

namespace ShoppingCart.Infrastructure;

// dotnet add package StackExchange.Redis
internal class RedisShoppingCartRepository(IConnectionMultiplexer connectionMultiplexer) : IShoppingCartRepository
{
    private IDatabase db => connectionMultiplexer.GetDatabase();

    public async Task Add(CartItem cartItem)
    {
        string sessionId = "OxJah";

        string key = $"cart:{sessionId}";
        string field = cartItem.ProductName;

        await db.HashIncrementAsync(key, field, cartItem.Quantity);
    }

    public async Task Remove(CartItem cartItem)
    {
        string sessionId = "OxJah";

        string key = $"cart:{sessionId}";
        string field = cartItem.ProductName;

        await db.HashIncrementAsync(key, field, -cartItem.Quantity);
    }
}
