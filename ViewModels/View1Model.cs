using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class View1Model : ObservableRecipient, IRecipient<ValueChangedMessage<UserDetails?>>
{
    private readonly IAuthService _authService;

    public View1Model(IAuthService authService)
    {
        IsActive = true;
        _authService = authService;
        Content = GetContent();
    }

    public void Receive(ValueChangedMessage<UserDetails?> message)
    {
        Content = GetContent();
    }

    private object? GetContent()
    {
        return _authService.IsAuthorized
            ? new UserDetailsViewModel(_authService.UserInfo!)
            : new UnAuthorizedModel();
    }

    [ObservableProperty]
    private object? _content;
}