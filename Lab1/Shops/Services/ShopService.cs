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
            var shop = new Shop(shopName, address);
            _shopList.Add(shop);
            return shop;
        }

        public void BuyProduct(Shop shop, Customer customer, CartItem cartItem)
        {
            if (!shop.ContainsProduct(cartItem.Product))
            {
                throw new ShopException("Product is not in shop");
            }

            if (shop.Products[cartItem.Product].Quantity < cartItem.Quantity || cartItem.Quantity == 0)
            {
                throw new ShopException("Not enough products in stock");
            }

            decimal totalCost = shop.Products[cartItem.Product].Price.Value * cartItem.Quantity;

            customer.SubstractMoney(totalCost);
            shop.BuyProduct(cartItem);
        }

        public void BuyProducts(Shop shop, Customer customer, List<CartItem> products)
        {
            decimal totalPrice = CalculatePrice(shop, products);
            if (customer.Money < totalPrice)
            {
                throw new ShopException("Not enough money");
            }

            shop.BuyProducts(products);
            customer.SubstractMoney(totalPrice);
        }

        public void CreateDelivery(Shop shop, Dictionary<Product, ProductInfo> products)
        {
            if (!ShopExists(shop))
            {
                throw new ShopException("Shop does not exist");
            }

            foreach (KeyValuePair<Product, ProductInfo> product in products)
            {
                shop.AddProduct(new CartItem(product.Key, product.Value.Quantity), product.Value.Price.Value);
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

        public Shop SearchBestPrice(List<CartItem> cartItems)
        {
            Shop? bestShop = null;
            decimal bestPrice = decimal.MaxValue;

            foreach (Shop shop in _shopList)
            {
                try
                {
                    decimal price = CalculatePrice(shop, cartItems);
                    if (price >= bestPrice) continue;
                    bestPrice = price;
                    bestShop = shop;
                }
                catch (ShopException)
                {
                    continue;
                }
            }

            return bestShop ?? throw new ShopException("No shop found for your purchase");
        }

        private decimal CalculatePrice(Shop shop, List<CartItem> products)
        {
            decimal price = 0;
            foreach (var product in products)
            {
                if (!shop.ContainsProduct(product.Product))
                {
                    throw new ShopException("Product is not in shop");
                }

                if (shop.Products[product.Product].Quantity < product.Quantity || product.Quantity == 0)
                {
                    throw new ShopException("Not enough products in stock");
                }

                price += shop.Products[product.Product].Price.Value * product.Quantity;
            }

            return price;
        }

        private bool ShopExists(Shop shop) => _shopList.Contains(shop);
    }
}