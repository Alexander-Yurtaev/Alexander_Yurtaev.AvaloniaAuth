using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class RegisterDialogViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;

    public RegisterDialogViewModel(IAuthService authService, IDialogService dialogService)
    {
        _authService = authService;
        _dialogService = dialogService;
    }

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    public async Task Cancel()
    {
        _dialogService.CloseCurrentDialog(null);
    }

    [RelayCommand]
    public async Task Register()
    {
        var result = await _authService.RegisterAsync(Email, Name, Password);
        if (result == true)
        {
            var user = new UserForLogin(Email, Password);
            _dialogService.CloseCurrentDialog(user);
        }        
    }
}
