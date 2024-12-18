using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Domain.Entities;

internal class Product  : BaseEntity
{    
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountedPrice { get; set; }
    public string Owner { get; set; }

    public Product(int id, string name, decimal price, decimal discountedPrice = 0) 
        : base(id)
        
    {
        Name = name;
        Price = price;
        DiscountedPrice = discountedPrice;
        Owner = "john";
    }
}
