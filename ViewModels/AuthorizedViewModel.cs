using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class AuthorizedViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<User?>>
{
    private readonly IAuthService _authService;

    [ObservableProperty]
    private AuthorizedModel? _model;

    public AuthorizedViewModel(IAuthService authService)
    {
        IsActive = true;
        _authService = authService;
        Model = GetModel();
    }

    [RelayCommand]
    public async Task Logout()
    {
        await _authService.LogoutAsync();
    }

    public void Receive(ValueChangedMessage<User?> message)
    {
        Model = GetModel();
    }

    private AuthorizedModel? GetModel()
    {
        return (_authService.User is not null) 
            ? new AuthorizedModel(_authService.User.Email) 
            : null;
    }
}
