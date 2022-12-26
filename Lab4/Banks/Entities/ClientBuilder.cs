namespace Banks.Entities
{
    using Banks.Exceptions;

    public class ClientBuilder
    {
        private string? _name;
        private string? _surname;
        private string _address = string.Empty;
        private string _passport = string.Empty;

        public ClientBuilder SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ClientException("Name cannot be null or empty");
            }

            _name = name;
            return this;
        }

        public void SetSurname(string surname)
        {
            if (string.IsNullOrEmpty(surname))
            {
                throw new ClientException("Surname cannot be null or empty");
            }

            _surname = surname;
        }

        public void SetAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ClientException("Address cannot be null or empty");
            }

            _address = address;
        }

        public void SetPassport(string passport)
        {
            if (string.IsNullOrEmpty(passport))
            {
                throw new ClientException("Passport cannot be null or empty");
            }

            _passport = passport;
        }

        public Client Build()
        {
            if (string.IsNullOrEmpty(_name) || string.IsNullOrEmpty(_surname))
                throw new ClientException("Client cannot be created without all fields");

            return new Client(_name, _surname, _address, _passport);
        }
    }
}