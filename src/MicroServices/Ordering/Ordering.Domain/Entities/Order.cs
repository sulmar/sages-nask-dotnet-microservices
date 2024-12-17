namespace Ordering.Domain.Entities;

internal class Order
{
    public string Number { get; set; }
    public OrderState State { get; set; }
}

internal enum OrderState
{
    Created,
    Shipping,
    Shipped,
    Cancelled
}
