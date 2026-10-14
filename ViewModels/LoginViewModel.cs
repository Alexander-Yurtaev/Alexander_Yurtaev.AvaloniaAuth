using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class LoginViewModel : ObservableValidator
{
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;
    private readonly Func<MainWindow> _mainWindowFactory;

    public LoginViewModel(
        IAuthService authService, 
        IDialogService dialogService,
        Func<MainWindow> mainWindowFactory)
    {
        _authService = authService;
        _dialogService = dialogService;
        _mainWindowFactory = mainWindowFactory;
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
        var owner = _mainWindowFactory();
        var user = await _dialogService.ShowRegisterDialog(owner);
        if (user is not null)
        {
            await _authService.LoginAsync(user.Email, user.Password);
        }
    }
}