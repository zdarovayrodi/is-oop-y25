using Shops.Tools;

namespace Shops.Models;

public class ProductPrice
{
    public decimal Value { get; private init; }

    public ProductPrice(decimal price)
    {
        if (price < 0)
            throw new ProductException("Price can't be negative");
        Value = price;
    }
}