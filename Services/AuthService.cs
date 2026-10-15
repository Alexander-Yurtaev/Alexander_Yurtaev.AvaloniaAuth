using Alexander_Yurtaev.AvaloniaAuth.Data;
using Alexander_Yurtaev.AvaloniaAuth.Dto;
using Alexander_Yurtaev.AvaloniaAuth.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public class AuthService : ObservableObject, IAuthService
{
    private readonly IDataProvider _dataProvider;
    private TokensModel? _tokensModel;

    public AuthService(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public bool IsAuthorized => User is not null;
    
    public UserDetails? User { get; private set; }

    public async Task<RegisteredUserDto> RegisterAsync(string email, string name, string password, CancellationToken ct)
    {
        var response = await _dataProvider.RegisterAsync(email, name, password, ct)
            ?? 
            throw new NoNullAllowedException("Ошибка при регистрации");
        
        if (!string.IsNullOrEmpty(response.Error))
        {
            throw new InvalidOperationException(response.Message);
        }

        var result = new RegisteredUserDto(response.Email, password);
        return result;
    }

    public async Task LoginAsync(string email, string password, CancellationToken ct)
    {
        var tokensModel = await _dataProvider.LoginAsync(email, password, ct);
        if (tokensModel is not null)
        {
            _tokensModel = new TokensModel(tokensModel.AccessToken, tokensModel.RefreshToken);
            var user = await _dataProvider.GetUserAsync(_tokensModel.AccessToken, ct);
            User = user;
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<UserDetails?>(User));
        }
    }

    public async Task LoadUserAsync(string accessToken, CancellationToken ct)
    {
        User = await _dataProvider.GetUserAsync(_tokensModel!.AccessToken, ct);
        WeakReferenceMessenger.Default.Send(new ValueChangedMessage<UserDetails?>(User));
    }

    public async Task LogoutAsync(CancellationToken ct)
    {
        await _dataProvider.LogoutAsync(_tokensModel!.RefreshToken, ct);
        User = null;
        _tokensModel = null;
        WeakReferenceMessenger.Default.Send(new ValueChangedMessage<UserDetails?>(User));
    }
}