using Shops.Models;

namespace Shops.Test
{
    using Shops.Entities;
    using Shops.Services;
    using Shops.Tools;
    using Xunit;

    public class ShopServiceTest
    {
        [Fact]
        public void ShopDelivery_CanBuy()
        {
            var shopService = new ShopService();
            Shop shop = shopService.AddShop("TestShop", "Test addr, 10a");
            var product1 = new Product("TestProduct1");
            var product2 = new Product("TestProduct2");
            var product3 = new Product("TestProduct3");
            var product4 = new Product("TestProduct4");
            var customer = new Customer("Test Customer", 600);

            shopService.CreateDelivery(shop, new Dictionary<Product, ProductInfo>
            {
                { product1, new ProductInfo(100, 1) },
                { product2, new ProductInfo(200, 2) },
                { product3, new ProductInfo(300, 3) },
            });

            shopService.BuyProduct(shop, customer, new CartItem(product1));
            shopService.BuyProduct(shop, customer, new CartItem(product2, 2));

            Assert.Throws<CustomerException>(() => shopService.BuyProduct(shop, customer, new CartItem(product3)));
            Assert.Throws<ShopException>(() => shopService.BuyProduct(shop, customer, new CartItem(product4)));
            Assert.Throws<ShopException>(() => shopService.BuyProduct(shop, customer, new CartItem(product4, 100)));
            Assert.Equal(100, customer.Money);
            Assert.Equal(500, shop.ShopBalance);
        }

        [Fact]
        public void CanSetAndChangePrice()
        {
            var shopService = new ShopService();
            Shop shop = shopService.AddShop("TestShop", "Test addr, 11b");

            var product1 = new Product("TestProduct1");
            var product2 = new Product("TestProduct2");
            var product3 = new Product("TestProduct3");
            var product4 = new Product("TestProduct4");

            shopService.CreateDelivery(shop, new Dictionary<Product, ProductInfo>
            {
                { product1, new ProductInfo(100, 1) },
                { product2, new ProductInfo(200, 2) },
                { product3, new ProductInfo(300, 3) },
            });

            shopService.ChangePrice(shop, product1, 199);
            shopService.ChangePrice(shop, product1, 299);
            shopService.ChangePrice(shop, product2, 399);
            shopService.ChangePrice(shop, product3, 499);

            Assert.Throws<ShopException>(() => shopService.ChangePrice(shop, product4, 599));
            Assert.Equal(299, shop.Products[product1].Price.Value);
            Assert.Equal(399, shop.Products[product2].Price.Value);
            Assert.Equal(499, shop.Products[product3].Price.Value);
        }

        [Fact]
        public void CanSearchBestPrice_ThrowException()
        {
            var shopService = new ShopService();
            Shop shop1 = shopService.AddShop("TestShop1", "Test addr, 11b");
            Shop shop2 = shopService.AddShop("TestShop2", "Test addr, 12c");
            Shop shop3 = shopService.AddShop("TestShop3", "Test addr, 13d");

            var product1 = new Product("TestProduct1");
            var product2 = new Product("TestProduct2");
            var product3 = new Product("TestProduct3");
            var product4 = new Product("TestProduct4");

            shopService.CreateDelivery(shop1, new Dictionary<Product, ProductInfo>
            {
                { product1, new ProductInfo(100, 1) },
                { product2, new ProductInfo(200, 2) },
                { product3, new ProductInfo(300, 3) },
            });

            shopService.CreateDelivery(shop2, new Dictionary<Product, ProductInfo>
            {
                { product1, new ProductInfo(200, 1) },
                { product2, new ProductInfo(300, 2) },
                { product3, new ProductInfo(400, 3) },
            });

            shopService.CreateDelivery(shop3, new Dictionary<Product, ProductInfo>
            {
                { product1, new ProductInfo(10, 10) },
                { product2, new ProductInfo(20, 2) },
                { product3, new ProductInfo(30, 3) },
            });

            Shop bestShop = shopService.SearchBestPrice(new List<CartItem>
                { new CartItem(product1, 5) });

            Assert.Throws<ShopException>(() => shopService.SearchBestPrice(new List<CartItem>
                { new CartItem(product4, 100) }));
            Assert.Equal(shop3, bestShop);
        }

        [Fact]
        public void CanBuyBatchOfGoods_ThrowException()
        {
            var shopService = new ShopService();
            Shop shop1 = shopService.AddShop("TestShop1", "Test addr, 11b");
            var customer = new Customer("Test Customer", 600);

            var product1 = new Product("TestProduct1");
            var product2 = new Product("TestProduct2");
            var product3 = new Product("TestProduct3");
            var product4 = new Product("TestProduct4");

            shopService.CreateDelivery(shop1, new Dictionary<Product, ProductInfo>
            {
                { product1, new ProductInfo(100, 1) },
                { product2, new ProductInfo(200, 2) },
                { product3, new ProductInfo(300, 3) },
            });
            shopService.BuyProducts(shop1, customer, new List<CartItem>
            {
                new CartItem(product1),
                new CartItem(product2),
                new CartItem(product3),
            });

            Assert.Equal(0, customer.Money);
            Assert.Equal(600, shop1.ShopBalance);

            customer.AddMoney(600);

            Assert.Throws<ShopException>(() => shopService.BuyProducts(shop1, customer, new List<CartItem>
            {
                new CartItem(product1),
                new CartItem(product2),
                new CartItem(product3),
                new CartItem(product4),
            }));
            Assert.Equal(600, customer.Money);
        }
    }
}