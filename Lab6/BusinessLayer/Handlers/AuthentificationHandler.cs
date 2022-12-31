using BusinessLayer.Service;

namespace BusinessLayer.Handlers;

public static class AuthentificationHandler
{
    public static bool Authentification(MessageService service, string username, string password)
    {
        return service.Accounts.Any(a => a.Username == username && a.IsCorrectPassword(password));
    }
}