namespace Shops.Entities
{
    using Shops.Tools;

    public class Product
    {
        public Product(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ShopException("Invalid name");

            Name = name;
        }

        public string Name { get; private set; }
    }
}