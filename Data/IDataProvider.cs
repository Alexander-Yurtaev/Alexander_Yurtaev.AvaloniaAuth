using Alexander_Yurtaev.AvaloniaAuth.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Data;

public interface IDataProvider
{
    Task<RegisterResponse?> RegisterAsync(string email, string name, string password, CancellationToken ct);
    Task<LoginResponse?> LoginAsync(string email, string password, CancellationToken ct);
    Task<MeResponse?> GetUserAsync(string accessToken, CancellationToken ct);
    Task<bool?> LogoutAsync(string refreshToken, CancellationToken ct);
}
