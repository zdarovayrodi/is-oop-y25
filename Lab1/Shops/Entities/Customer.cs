using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        private decimal _money;
        private string _name = string.Empty;

        public Customer(string name, decimal money)
        {
            Money = money;
            Name = name;
        }

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new CustomerException("Name cannot be empty");
                }

                _name = value;
            }
        }

        public decimal Money
        {
            get => _money;
            private set
            {
                if (value < 0)
                {
                    throw new CustomerException("Money cannot be negative");
                }

                _money = value;
            }
        }

        public void AddMoney(decimal money)
        {
            Money += money;
        }

        public void SubstractMoney(decimal money)
        {
            if (Money - money < 0)
            {
                throw new CustomerException("Customer doesn't have enough money");
            }

            Money -= money;
        }
    }
}