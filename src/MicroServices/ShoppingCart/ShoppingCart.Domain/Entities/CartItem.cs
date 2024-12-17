namespace ShoppingCart.Domain.Entities;

public record CartItem(string ProductName, int Quantity = 1);

public record Cart
{
    public ICollection<CartItem> Items { get; set; } = [];
}
