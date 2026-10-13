using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class View1Model : ObservableRecipient, IRecipient<ValueChangedMessage<User?>>
{
    private readonly IAuthService _authService;
    private readonly UnAuthorizedModel _unAthorizedModel;

    [ObservableProperty]
    private object? _content;

    public View1Model(IAuthService authService)
    {
        IsActive = true; // Start receiving messages
        _authService = authService;
        _authService.LoginAsync("email", "12345");
        _unAthorizedModel = new UnAuthorizedModel();
    }

    public void Receive(ValueChangedMessage<User?> message)
    {
        Content = _authService.IsAuthorized ? _authService.User : _unAthorizedModel;
    }
}