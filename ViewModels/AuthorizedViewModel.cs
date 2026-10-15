using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class AuthorizedViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<UserDetails?>>
{
    private readonly IAuthService _authService;
    private CancellationTokenSource? _cts;

    [ObservableProperty]
    private string _title = "Вы вошли как";

    [ObservableProperty]
    private string _message = string.Empty;

    public AuthorizedViewModel(IAuthService authService)
    {
        IsActive = true;
        _authService = authService;
    }

    public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [RelayCommand]
    public async Task Logout()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        try
        {
            await _authService.LogoutAsync(_cts.Token);
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ErrorMessage = ex.Message;
            OnPropertyChanged(nameof(HasErrorMessage));
        }
    }

    public void Receive(ValueChangedMessage<UserDetails?> message)
    {
        Message = (_authService.IsAuthorized)
            ? _authService.UserInfo!.Email
            : string.Empty;
    }
}