namespace Banks.Entities
{
    using Banks.Entities.Interfaces;
    using Banks.Observer;

    public class Client : IObserver
    {
        private List<string> _updates = new List<string>();
        public Client(string name, string surname, string address = "", string passport = "")
        {
            Name = name;
            Surname = surname;
            Address = address;
            Passport = passport;
        }

        public string Name { get; }
        public string Surname { get; }
        public string Address { get; private set; }
        public string Passport { get; private set; }
        public bool IsSuspicious => Address == string.Empty || Passport == string.Empty;

        public void SetAddress(string address)
        {
            if (string.IsNullOrEmpty(address) || string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address cannot be null or empty");
            Address = address;
        }

        public void SetPassport(string passport)
        {
            if (string.IsNullOrEmpty(passport) || string.IsNullOrWhiteSpace(passport))
                throw new ArgumentException("Passport cannot be null or empty");
            Passport = passport;
        }

        public void Update(string message)
        {
            _updates.Add(message);
        }
    }
}