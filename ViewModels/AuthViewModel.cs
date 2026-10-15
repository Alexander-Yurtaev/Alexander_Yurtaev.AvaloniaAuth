using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class AuthViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<UserDetails?>>
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

    public void Receive(ValueChangedMessage<UserDetails?> message)
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
