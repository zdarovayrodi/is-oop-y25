using Shops.Tools;

namespace Shops.Entities
{
    using Shops.Models;

    public class Shop
    {
        public Shop(string shopName, Guid shopId, string address)
        {
            if (string.IsNullOrEmpty(shopName))
            {
                throw new ShopException("Shop name cannot be null or empty");
            }

            if (string.IsNullOrEmpty(address))
            {
                throw new ShopException("Shop address cannot be null or empty");
            }

            ShopName = shopName;
            ShopId = shopId;
            ShopAddress = address;
        }

        public string ShopName { get; private set; }
        public Guid ShopId { get; private set; }
        public string ShopAddress { get; private set; }

        public decimal ShopBalance { get; private set; } = 0;

        // (product : (price, quantity))
        public Dictionary<Product, ProductInfo> Products { get; private set; } = new Dictionary<Product, ProductInfo>();

        public void AddProduct(Product product, decimal price, uint quantity)
        {
            if (ContainsProduct(product))
            {
                Products[product].Quantity += quantity;
                Products[product].Price = price;
                return;
            }

            var productInfo = new ProductInfo(price, quantity);
            Products.Add(product, productInfo);
        }

        public void BuyProduct(Product product, uint quantity)
        {
            if (!ContainsProduct(product))
            {
                throw new ShopException("Product not found");
            }

            if (Products[product].Quantity < quantity)
            {
                throw new ShopException("Not enough products in stock");
            }

            Products[product].Quantity -= quantity;
            ShopBalance += Products[product].Price * quantity;
        }

        public void BuyProducts(Dictionary<Product, uint> productList)
        {
            // all products are available
            foreach (var product in productList)
            {
                if (!ContainsProduct(product.Key))
                {
                    throw new ShopException("Product not found");
                }

                if (Products[product.Key].Quantity < product.Value)
                {
                    throw new ShopException("Not enough products in stock");
                }
            }

            decimal totalPrice = productList.Sum(product => Products[product.Key].Price * product.Value);
            ShopBalance += totalPrice;
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

            Products[product].Price = newPrice;
        }

        public bool ContainsProduct(Product product) => Products.ContainsKey(product);
    }
}