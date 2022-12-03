namespace Banks.Entities.Interfaces
{
    public interface IClient
    {
        string Name { get; }
        string Surname { get; }
        string Address { get; }
        string Passport { get; }
    }
}