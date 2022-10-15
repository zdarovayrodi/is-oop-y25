namespace Shops.Entities
{
    using Shops.Models;
    using Shops.Tools;

    public class Shop
    {
        public Shop(string shopName, string address)
        {
            if (string.IsNullOrWhiteSpace(shopName))
            {
                throw new ShopException("Shop name cannot be null or empty");
            }

            if (string.IsNullOrEmpty(address))
            {
                throw new ShopException("Shop address cannot be null or empty");
            }

            ShopName = shopName;
            ShopAddress = address;
            ShopId = Guid.NewGuid();
        }

        public string ShopName { get; private set; }
        public Guid ShopId { get; private init; }
        public string ShopAddress { get; private init; }

        public decimal ShopBalance { get; private set; } = 0;

        public Dictionary<Product, ProductInfo> Products { get; private set; } = new Dictionary<Product, ProductInfo>();

        public void AddProduct(CartItem item, decimal price)
        {
            if (ContainsProduct(item.Product))
            {
                Products[item.Product].AddQuantity(item.Quantity);
                Products[item.Product].ChangePrice(price);
                return;
            }

            var productInfo = new ProductInfo(price, item.Quantity);
            Products.Add(item.Product, productInfo);
        }

        public void BuyProduct(CartItem cartItem)
        {
            if (!ContainsProduct(cartItem.Product))
            {
                throw new ShopException("Product not found");
            }

            if (Products[cartItem.Product].Quantity < cartItem.Quantity)
            {
                throw new ShopException("Not enough products in stock");
            }

            Products[cartItem.Product].RemoveQuantity(cartItem.Quantity);
            ShopBalance += Products[cartItem.Product].Price.Value * cartItem.Quantity;
        }

        public void BuyProducts(List<CartItem> cartItems)
        {
            decimal totalCost = 0;
            foreach (CartItem cartItem in cartItems)
            {
                if (!ContainsProduct(cartItem.Product) || (Products[cartItem.Product].Quantity < cartItem.Quantity))
                {
                    throw new ShopException("Product not found or not enough products in stock");
                }

                totalCost += Products[cartItem.Product].Price.Value * cartItem.Quantity;
            }

            foreach (CartItem cartItem in cartItems)
            {
                Products[cartItem.Product].RemoveQuantity(cartItem.Quantity);
            }

            ShopBalance += totalCost;
        }

        public void DeleteProduct(Product product)
        {
            if (!ContainsProduct(product))
            {
                throw new ShopException("Product not found");
            }

            Products.Remove(product);
        }

        public void ChangePrice(Product product, decimal newPrice)
        {
            if (!ContainsProduct(product))
            {
                throw new ShopException("Product not found");
            }

            Products[product].ChangePrice(newPrice);
        }

        public bool ContainsProduct(Product product) => Products.ContainsKey(product);
    }
}