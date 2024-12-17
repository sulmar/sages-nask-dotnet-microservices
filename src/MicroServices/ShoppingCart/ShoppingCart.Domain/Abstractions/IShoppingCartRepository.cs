using ShoppingCart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Abstractions;

internal interface IShoppingCartRepository
{
    Task Add(CartItem cartItem);
    Task Remove(CartItem cartItem);
}
