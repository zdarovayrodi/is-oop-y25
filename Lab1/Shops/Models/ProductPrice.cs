using Shops.Tools;

namespace Shops.Models;

public class ProductPrice
{
    public ProductPrice(decimal price)
    {
        if (price < 0)
            throw new ProductException("Price can't be negative");
        Value = price;
    }

    public decimal Value { get; private init; }
}