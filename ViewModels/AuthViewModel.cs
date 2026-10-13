using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class AuthViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<User?>>
{
    private readonly AuthorizedViewModel _authorizedViewModel;
    private readonly IAuthService _authService;

    [ObservableProperty]
    private object? _content;

    public AuthViewModel(
        IAuthService authService,
        AuthorizedViewModel authorizedViewModel)
    {
        IsActive = true;
        _authService = authService;
        _authorizedViewModel = authorizedViewModel;
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
            return App.Services!.GetRequiredService<LoginViewModel>();
        }
    }
}
