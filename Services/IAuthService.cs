using Alexander_Yurtaev.AvaloniaAuth.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public interface IAuthService
{
    bool IsAuthorized { get; }
    User? User { get; }

    Task<bool?> RegisterAsync(string email, string name, string password, CancellationToken ct);
    Task LoginAsync(string email, string password, CancellationToken ct);
    Task LoadUserAsync(string accessToken, CancellationToken ct);
    Task LogoutAsync(CancellationToken ct);
}