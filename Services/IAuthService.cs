using Alexander_Yurtaev.AvaloniaAuth.Dto;
using Alexander_Yurtaev.AvaloniaAuth.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public interface IAuthService
{
    bool IsAuthorized { get; }
    UserDetails? UserInfo { get; }

    Task<RegisteredUserDto> RegisterAsync(string email, string name, string password, CancellationToken ct);
    Task LoginAsync(string email, string password, CancellationToken ct);
    Task LoadUserAsync(CancellationToken ct);
    Task LogoutAsync(CancellationToken ct);
}