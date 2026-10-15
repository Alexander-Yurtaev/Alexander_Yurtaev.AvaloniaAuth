using Alexander_Yurtaev.AvaloniaAuth.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public interface IDataProvider
{
    Task<bool?> RegisterAsync(string email, string name, string password, CancellationToken ct);
    Task<TokensModel?> LoginAsync(string email, string password, CancellationToken ct);
    Task<User?> GetUserAsync(string accessToken, CancellationToken ct);
    Task<bool?> LogoutAsync(string refreshToken, CancellationToken ct);
}
