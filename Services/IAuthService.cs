using Alexander_Yurtaev.AvaloniaAuth.Models;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public interface IAuthService
{
    bool IsAuthorized { get; }
    User? User { get; }
    void Register(string email, string name, string password);
    void Login(string email, string password);
    void Logout();
}