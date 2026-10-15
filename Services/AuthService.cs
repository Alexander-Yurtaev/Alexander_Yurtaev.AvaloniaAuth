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
using System.Xml.Linq;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public class AuthService : ObservableObject, IAuthService
{
    private readonly IDataProvider _dataProvider;
    private TokensModel? _tokensModel;

    public AuthService(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public bool IsAuthorized => UserInfo is not null;
    
    public UserDetails? UserInfo { get; private set; }

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
        var response = await _dataProvider.LoginAsync(email, password, ct)
            ??
            throw new NoNullAllowedException("Ошибка при авторизации");

        if (!string.IsNullOrEmpty(response.Error))
        {
            throw new InvalidOperationException(response.Message);
        }

        if (string.IsNullOrEmpty(response.AccessToken) || string.IsNullOrEmpty(response.RefreshToken))
        {
            throw new InvalidOperationException("Ошибка при авторизации");
        }

        _tokensModel = new TokensModel(response.AccessToken, response.RefreshToken);
    }

    public async Task LoadUserAsync(CancellationToken ct)
    {
        var response = await _dataProvider.GetUserAsync(_tokensModel!.AccessToken, ct)
            ??
            throw new NoNullAllowedException("Ошибка при авторизации");

        if (!string.IsNullOrEmpty(response.Error))
        {
            throw new InvalidOperationException(response.Message);
        }

        UserInfo = new UserDetails(response.Id, response.Email, response.Name, response.Avatar, response.Role);
        WeakReferenceMessenger.Default.Send(new ValueChangedMessage<UserDetails?>(UserInfo));
    }

    public async Task LogoutAsync(CancellationToken ct)
    {
        await _dataProvider.LogoutAsync(_tokensModel!.RefreshToken, ct);
        UserInfo = null;
        _tokensModel = null;
        WeakReferenceMessenger.Default.Send(new ValueChangedMessage<UserDetails?>(UserInfo));
    }
}