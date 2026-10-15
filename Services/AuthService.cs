using Alexander_Yurtaev.AvaloniaAuth.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public class AuthService : ObservableObject, IAuthService
{
    private readonly IDataProvider _dataProvider;
    private TokensModel _tokensModel;

    public AuthService(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public bool IsAuthorized => User is not null;
    
    public User? User { get; private set; }

    public async Task<bool?> RegisterAsync(string email, string name, string password, CancellationToken ct)
    {
        var result = await _dataProvider.RegisterAsync(email, name, password, ct);
        return result;

        //var id = new Random().Next(100);
        //var source = @"avares://Alexander.Yurtaev.Avalonia.Auth;component/Assets/AvatarBase.png";
        //User = new User(id, email, $"User_{id}", source, $"Role_{id}");
        //WeakReferenceMessenger.Default.Send(new ValueChangedMessage<User?>(User));
        //return await Task.FromResult(User != null);
    }

    public async Task LoginAsync(string email, string password, CancellationToken ct)
    {
        //var id = new Random().Next(100);
        //var source = @"avares://Alexander.Yurtaev.Avalonia.Auth;component/Assets/AvatarBase.png";
        //User = new User(id, email, $"User_{id}", source, $"Role_{id}");
        //WeakReferenceMessenger.Default.Send(new ValueChangedMessage<User?>(User));
        //await Task.Delay(0);

        var tokensModel = await _dataProvider.LoginAsync(email, password, ct);
        if (tokensModel is not null)
        {
            _tokensModel = new TokensModel(tokensModel.AccessToken, tokensModel.RefreshToken);
            var user = await _dataProvider.GetUserAsync(_tokensModel.AccessToken, ct);
            User = user;
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<User?>(User));
        }
    }

    public async Task LoadUserAsync(string accessToken, CancellationToken ct)
    {
        //var id = new Random().Next(100);
        //var source = @"avares://Alexander.Yurtaev.Avalonia.Auth;component/Assets/AvatarBase.png";
        //User = new User(id, email, $"User_{id}", source, $"Role_{id}");
        //return User;

        User = await _dataProvider.GetUserAsync(_tokensModel.AccessToken, ct);
        WeakReferenceMessenger.Default.Send(new ValueChangedMessage<User?>(User));
    }

    public async Task LogoutAsync(CancellationToken ct)
    {
        await _dataProvider.LogoutAsync(_tokensModel.RefreshToken, ct);
        User = null;
        WeakReferenceMessenger.Default.Send(new ValueChangedMessage<User?>(User));
    }
}