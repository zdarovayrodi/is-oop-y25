using Shops.Tools;

namespace Shops.Models;

public class ProductInfo
{
    private decimal _price;

    public ProductInfo(decimal price, uint quantity)
    {
        Price = price;
        Quantity = quantity;
    }

    public uint Quantity { get; set; }
    public decimal Price
    {
        get => _price;

        set
        {
            if (value < 0) throw new ProductException("Price cannot be negative.");
            _price = value;
        }
    }
}