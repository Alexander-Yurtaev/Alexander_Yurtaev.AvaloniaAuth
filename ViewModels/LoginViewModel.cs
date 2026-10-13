using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class LoginViewModel : ObservableValidator
{
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;

    public LoginViewModel(IAuthService authService, IDialogService dialogService)
    {
        _authService = authService;
        _dialogService = dialogService;
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Введите Email.")]
    [EmailAddress(ErrorMessage = "Введите валидный Email.")]
    private string _email;

    partial void OnEmailChanged(string value)
    {
        ValidateProperty(value, nameof(Email));
        LoginCommand.NotifyCanExecuteChanged();
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Введите пароль.")]
    [MinLength(length: 8, ErrorMessage = "Длинна пароля должна быть не меньше 8 символов.")]
    private string _password;

    partial void OnPasswordChanged(string value)
    {
        ValidateProperty(value, nameof(Password));
        LoginCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    public async Task Login()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        await _authService.LoginAsync(Email, Password);
    }

    private bool CanSubmit() => !HasErrors;

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