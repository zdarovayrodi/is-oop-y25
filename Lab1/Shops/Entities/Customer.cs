using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        public Customer(string name, int money)
        {
            if (money < 0)
            {
                throw new CustomerException($"Money ({money}) cannot be negative");
            }

            Name = name;
            Money = money;
        }

        public string Name { get; private set; }
        public int Money { get; private set; }

        public void AddMoney(int money) => Money += money;
    }
}