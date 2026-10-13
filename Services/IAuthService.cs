using Alexander_Yurtaev.AvaloniaAuth.Models;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public interface IAuthService
{
    bool IsAuthorized { get; }
    User? User { get; }

    Task RegisterAsync(string email, string name, string password);
    Task LoginAsync(string email, string password);
    Task LogoutAsync();
}