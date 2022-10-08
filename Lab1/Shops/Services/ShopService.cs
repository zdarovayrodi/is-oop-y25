using Shops.Models;
using Shops.Tools;

namespace Shops.Services
{
    using Shops.Entities;

    public class ShopService
    {
        private List<Shop> _shopList = new List<Shop>();

        public Shop AddShop(string shopName, string address)
        {
            var shopId = Guid.NewGuid();
            var shop = new Shop(shopName, shopId, address);
            _shopList.Add(shop);
            return shop;
        }

        public void BuyProduct(Shop shop, Customer customer, Product product, uint quantity)
        {
            if (!shop.ContainsProduct(product))
            {
                throw new ShopException("Product is not in shop");
            }

            if (shop.Products[product].Quantity < quantity || quantity == 0)
            {
                throw new ShopException("Not enough products in stock");
            }

            decimal totalCost = shop.Products[product].Price * quantity;

            customer.SubstractMoney(totalCost);
            shop.BuyProduct(product, quantity);
        }

        public void BuyProducts(Shop shop, Customer customer, Dictionary<Product, uint> products)
        {
            decimal totalPrice = CalculatePrice(shop, products);
            if (customer.Money < totalPrice)
            {
                throw new ShopException("Not enough money");
            }

            shop.BuyProducts(products);
            customer.SubstractMoney(totalPrice);
        }

        public void CreateDelivery(Shop shop, Dictionary<Product, (decimal, uint)> products)
        {
            if (!ShopExists(shop))
            {
                throw new ShopException("Shop does not exist");
            }

            foreach (var product in products)
            {
                shop.AddProduct(product.Key, product.Value.Item1, product.Value.Item2);
            }
        }

        public void ChangePrice(Shop shop, Product product, uint newPrice)
        {
            if (!ShopExists(shop))
            {
                throw new ShopException("Shop does not exist");
            }

            shop.ChangePrice(product, newPrice);
        }

        public Shop SearchBestPrice(Product product, uint quantity)
        {
            Shop? bestShop = null;
            decimal bestPrice = decimal.MaxValue;

            foreach (var shop in _shopList.Where(shop => shop.ContainsProduct(product) &&
                     shop.Products[product].Quantity >= quantity).Where(shop => shop.Products[product].Price < bestPrice))
            {
                bestPrice = shop.Products[product].Price;
                bestShop = shop;
            }

            return bestShop ?? throw new ShopException("No shop has this product");
        }

        private decimal CalculatePrice(Shop shop, Dictionary<Product, uint> products)
        {
            decimal price = 0;
            foreach (var product in products)
            {
                if (!shop.ContainsProduct(product.Key))
                {
                    throw new ShopException("Product is not in shop");
                }

                if (shop.Products[product.Key].Quantity < product.Value)
                {
                    throw new ShopException("Not enough products in stock");
                }

                price += shop.Products[product.Key].Price * product.Value;
            }

            return price;
        }

        private bool ShopExists(Shop shop) => _shopList.Contains(shop);
    }
}