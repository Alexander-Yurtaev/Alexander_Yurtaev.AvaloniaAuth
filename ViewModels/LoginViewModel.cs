using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;

    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;
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
        var user = await _authService.RegisterAsync(Email, "", Password);
        if (user is null) return;
        await _authService.LoginAsync(Email, Password);
    }
}
