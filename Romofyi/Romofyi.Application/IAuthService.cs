namespace Romofyi.Application
{
    public interface IAuthService
    {
        Customer AuthenticateUser(string username, string password);
        bool HasRequiredRole(Customer user, string role);
        Customer RegisterUser(string username, string password);
    }
}