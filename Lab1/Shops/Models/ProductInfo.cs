using Shops.Tools;

namespace Shops.Models;

public class ProductInfo
{
    public ProductInfo(decimal price, uint quantity)
    {
        Price = new ProductPrice(price);
        Quantity = quantity;
    }

    public uint Quantity { get; private set; }

    public ProductPrice Price { get; private set; }

    public void AddQuantity(uint quantity) => Quantity += quantity;

    public void RemoveQuantity(uint quantity) => Quantity -= quantity;

    public void ChangePrice(decimal newPrice) => Price = new ProductPrice(newPrice);
}