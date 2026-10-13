using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;

    public LoginViewModel(IAuthService authService, IDialogService dialogService)
    {
        _authService = authService;
        _dialogService = dialogService;
    }

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    public async Task Login()
    {
        await _authService.LoginAsync(Email, Password);
    }

    [RelayCommand]
    public async Task Register()
    {
        var user = await _dialogService.ShowRegisterDialog(App.Services!.GetRequiredService<MainWindow>());
        if (user is not null)
        {
            await _authService.LoginAsync(user.Email, user.Password);
        }
    }
}