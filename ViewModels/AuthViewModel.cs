using Alexander_Yurtaev.AvaloniaAuth.Messages;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class AuthViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<UserInfoChangedMessage>>
{
    private readonly AuthorizedViewModel _authorizedViewModel;
    private readonly Func<LoginViewModel> _loginViewModelFactory;
    private readonly IAuthService _authService;

    [ObservableProperty]
    private object? _content;

    public AuthViewModel(
        IAuthService authService,
        AuthorizedViewModel authorizedViewModel,
        Func<LoginViewModel> factory)
    {
        IsActive = true;
        _authService = authService;
        _authorizedViewModel = authorizedViewModel;
        _loginViewModelFactory = factory;
        Content = GetContent();
    }

    public void Receive(ValueChangedMessage<UserInfoChangedMessage> message)
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
            return _loginViewModelFactory();
        }
    }
}
