using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        private decimal _money;

        public Customer(string name, decimal money)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new CustomerException("Name cannot be empty");
            }

            Money = money;
            Name = name;
        }

        public string Name { get; private set; }

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
            if (money <= 0)
            {
                throw new CustomerException("Wrong argument");
            }

            Money += money;
        }

        public void SubstractMoney(decimal money)
        {
            if (money <= 0)
            {
                throw new CustomerException("Wrong argument");
            }

            Money -= money;
        }
    }
}