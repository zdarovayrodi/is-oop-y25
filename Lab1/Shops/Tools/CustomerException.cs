namespace Shops.Tools;

public class CustomerException : Exception
{
    public CustomerException(string message)
        : base(message)
    {
    }
}