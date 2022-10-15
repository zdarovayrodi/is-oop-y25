namespace Shops.Models
{
    using Shops.Entities;

    public class CartItem
    {
        public CartItem(Product product, uint quantity = 1)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; private init; }
        public uint Quantity { get; private set; }

        public void AddQuantity(uint quantity)
        {
            Quantity += quantity;
        }

        public void RemoveQuantity(uint quantity)
        {
            Quantity -= quantity;
        }
    }
}