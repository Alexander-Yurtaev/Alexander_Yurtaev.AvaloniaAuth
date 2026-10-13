using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class AuthViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<User?>>
{
    private readonly AuthorizedViewModel _authorizedViewModel;
    private readonly LoginViewModel _loginViewModel;
    private readonly IAuthService _authService;

    [ObservableProperty]
    private object? _content;

    public AuthViewModel(
        IAuthService authService,
        AuthorizedViewModel authorizedViewModel,
        LoginViewModel loginViewModel)
    {
        IsActive = true;
        _authService = authService;
        _authorizedViewModel = authorizedViewModel;
        _loginViewModel = loginViewModel;
        Content = GetContent();
    }

    public void Receive(ValueChangedMessage<User?> message)
    {
        Content = GetContent();
    }

    private object? GetContent()
    {
        if (_authService.IsAuthorized)
        {
            return _authorizedViewModel;
        } else
        {
            return _loginViewModel;
        }
    }
}
